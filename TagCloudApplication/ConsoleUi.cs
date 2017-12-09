using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using TagsCloud;
using Autofac;
using TagsCloud.Infrastructure;
using TagsCloud.TextAnalyzing;
using TagsCloud.Vizualization;
using System.Collections.Generic;

namespace TagCloudApplication
{
    public class ConsoleUi
    {
        private readonly TagCloudSettings settings;
        private ITextReader textReader;
        private TagCloudTextAnalyzer textAnalyzer;
        private TagCloudDrawer drawer;
        private IImageSaver imageSaver;

        private string boringWordsPath;
        private bool useRandomColors;
        private HashSet<string> boringWords;
        public ConsoleUi() => settings = new TagCloudSettings();

        public void Run()
        {
            Console.WriteLine("CUI mode enabled!");

            ReadArguments();
            InitializeTypes();
            DrawWordCloud();

            Console.WriteLine("Finished!");
        }

        private void DrawWordCloud()
        {
            var text = textReader.ReadText(settings.InputPath);
            var words = textAnalyzer.GetWords(text, settings.TopNWords, settings.MinWordLength, 
                settings.MinFontSize, settings.MaxFontSize, settings.FontFamily);
            var bitmap = drawer.DrawTagCloud(words);
            imageSaver.SaveImage(bitmap, settings.OutputPath);
        }

        private void InitializeTypes()
        {
            var container = RegisterTypes();
            textReader = container.Resolve<ITextReader>();
            textAnalyzer = container.Resolve<TagCloudTextAnalyzer>();
            drawer = container.Resolve<TagCloudDrawer>();
            imageSaver = container.Resolve<IImageSaver>();
        }

        private IContainer RegisterTypes()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterAssemblyTypes(typeof(TagCloudSettings).Assembly)
                .Except<IWordFilter>()
                .Except<IWordConverter>()
                .Except<IColorSelector>()
                .WithParameter("center", Point.Empty)
                .AsSelf()
                .AsImplementedInterfaces();

            RegisterColorSelector(builder);
            RegisterWordFilters(builder);
            RegisterWordConverters(builder);

            return builder.Build();
        }

        private void RegisterColorSelector(ContainerBuilder builder)
        {
            if (useRandomColors)
                builder.RegisterType<RandomColorSelector>().As<IColorSelector>();
            else
                builder.RegisterType<OneColorSelector>().As<IColorSelector>().WithParameter("brush", Brushes.Black);
        }

        private void RegisterWordFilters(ContainerBuilder builder)
        {
            boringWords = File.ReadLines(boringWordsPath).ToHashSet();
            builder.RegisterType<SimpleWordFilter>().AsSelf().As<IWordFilter>().WithParameter("boringWords", boringWords);
        }

        private void RegisterWordConverters(ContainerBuilder builder)
        {
            builder.RegisterType<SimpleWordConverter>().As<IWordConverter>();
        }

        private void ReadArguments()
        {
            var cwd = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent?.FullName + "\\";
            settings.InputPath = ReadStringArgument("Path to file with text:", 
                defaultValue: cwd + @"Data\Holmes.txt");
            settings.OutputPath = ReadStringArgument("Save to (with name):", 
                defaultValue: cwd + @"Data\WordCloud");
            boringWordsPath = ReadStringArgument("Filter file path:", 
                defaultValue: cwd + @"Data\boring_words.txt");
           settings.TopNWords = ReadIntArgument("Use top N words (0 - all):", defaultValue: settings.TopNWords);
           settings.MinWordLength = ReadIntArgument("Min word length:", defaultValue: settings.MinWordLength);
           settings.FontFamily = ReadStringArgument("Font", defaultValue:settings.FontFamily);
           settings.MinFontSize = ReadIntArgument("Min Font Size:", defaultValue:settings.MinFontSize);
           settings.MaxFontSize = ReadIntArgument("Max Font Size:", defaultValue:settings.MaxFontSize);
           useRandomColors =  Convert.ToBoolean(ReadIntArgument("Use Random colors?[0=false else true]:", defaultValue: 0));
        }

        private static string ReadStringArgument(string msg, string defaultValue)
        {
            Console.WriteLine($"{msg} [Default={defaultValue}]");
            var arg = Console.ReadLine();
            return !string.IsNullOrEmpty(arg) ? arg : defaultValue;
        }

        private static int ReadIntArgument(string msg, int defaultValue)
        {
            Console.WriteLine($"{msg} [Default={defaultValue}]");
            var arg = Console.ReadLine();
            if (string.IsNullOrEmpty(arg)) return defaultValue;
            if (int.TryParse(arg, out var result))
                return result;
            throw new FormatException("Cant Parse Your Argument! Try again");
        }
    }
}