using System;
using System.IO;
using TagsCloud;
using TagsCloud.Infrastructure;

namespace TagCloudApplication.UI
{
    public class ConsoleUi : IUi
    {
        private readonly TagCloudHelper helper;

        public ConsoleUi(TagCloudHelper tagCloudHelper) => helper = tagCloudHelper;


        public void Run()
        {
            Console.WriteLine("CUI mode enabled!");
            ReadArguments();
            CreateWordCloud();
            Console.WriteLine("Finished!");
            Console.ReadKey();
        }

        private void CreateWordCloud()
        {
            helper.GetText()
                .Then(text => helper.GetWords(text))
                .RefineError("Can't get words: ")
                .Then(words => helper.GetTagCloudBitmap(words))
                .RefineError("Can't visualize words: ")
                .Then(image => helper.SaveImage(image))
                .RefineError("Can't save image: ")
                .OnFail(Console.WriteLine);
        }

        private void ReadArguments()
        {
            var cwd = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent?.FullName + "\\";
            helper.Settings.InputPath = ReadStringArgument("Path to file with text:",
                defaultValue: cwd + @"Data\Holmes.txt");
            helper.Settings.OutputPath = ReadStringArgument("Save to (with name):",
                defaultValue: cwd + @"Data\WordCloud");
            helper.Settings.TopNWords = ReadIntArgument("Use top N words (0 - all):", defaultValue: helper.Settings.TopNWords);
            helper.Settings.MinWordLength = ReadIntArgument("Min word length:", defaultValue: helper.Settings.MinWordLength);
            helper.Settings.FontFamily = ReadStringArgument("Font", defaultValue: helper.Settings.FontFamily);
            helper.Settings.MinFontSize = ReadIntArgument("Min Font Size:", defaultValue: helper.Settings.MinFontSize);
            helper.Settings.MaxFontSize = ReadIntArgument("Max Font Size:", defaultValue: helper.Settings.MaxFontSize);
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