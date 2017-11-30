using System;
using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud
{
    public class ConsoleUi : IUi
    {
        private readonly ITextReader textReader;
        private readonly TagCloud tagCloud;
        private readonly IImagaSaver imagaSaver;
        private string filepath;

        public ConsoleUi(ITextReader textReader, TagCloud tagCloud, IImagaSaver imagaSaver)
        {
            this.textReader = textReader;
            this.tagCloud = tagCloud;
            this.imagaSaver = imagaSaver;
        }

        public void Run()
        {
            Console.WriteLine("CUI mode enabled!");
            ReadArguments();
            CreateWordCloud();
            Console.WriteLine("Finished!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void CreateWordCloud()
        {
            var text = textReader.ReadText();
            var bitmap = tagCloud.DrawTagCloud(text);
            imagaSaver.SaveImage(bitmap, filepath);
        }

        private void ReadArguments()
        {
            var cwd = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.FullName + "\\";
            textReader.Filepath = ReadStringArgument("Path to file with text:", 
                defaultValue: cwd + @"VisualizationData\Hero of our Time.txt");
            filepath = ReadStringArgument("Save to (with name):", 
                defaultValue: cwd + @"VisualizationData\WCloud");
            tagCloud.TextAnalyzer.TopNWords = ReadIntArgument("Use top N words (0 - all):", defaultValue: 250);
            tagCloud.TextAnalyzer.MinWordLength = ReadIntArgument("Min word length:", defaultValue: 3);
            tagCloud.FontAnalyzer.FontFamily = ReadStringArgument("Font", defaultValue: "Calibri");
            tagCloud.FontAnalyzer.MinFontSize = ReadIntArgument("Min Font Size:", defaultValue: 20);
            tagCloud.FontAnalyzer.MaxFontSize = ReadIntArgument("Max Font Size:", defaultValue: 72);
        }

        private string ReadStringArgument(string msg, string defaultValue)
        {
            Console.WriteLine($"{msg} [Default={defaultValue}]");
            var arg = Console.ReadLine();
            if (!string.IsNullOrEmpty(arg)) return arg;
            if (defaultValue == null) throw new ArgumentException("This argument is reqired");
            return defaultValue;
        }

        private int ReadIntArgument(string msg, int defaultValue)
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