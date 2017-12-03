using System;
using System.IO;
using TagsCloud;

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
        }

        private void CreateWordCloud()
        {
            var text = helper.GetText();
            var boringWords = helper.GetBoringWords();
            var image = helper.DrawTagCould(text, boringWords);
            helper.Save(image);
        }

        private void ReadArguments()
        {
            var cwd = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent?.FullName + "\\";
            helper.InputPath = ReadStringArgument("Path to file with text:", 
                defaultValue: cwd + @"Data\Holmes.txt");
            helper.OutputPath = ReadStringArgument("Save to (with name):", 
                defaultValue: cwd + @"Data\WordCloud");
            helper.BoringWordsPath = ReadStringArgument("Filter file path:", 
                defaultValue: cwd + @"Data\boring_words.txt");
            helper.TopNWords = ReadIntArgument("Use top N words (0 - all):", defaultValue: helper.TopNWords);
            helper.MinWordLength = ReadIntArgument("Min word length:", defaultValue: helper.MinWordLength);
            helper.FontFamily = ReadStringArgument("Font", defaultValue: helper.FontFamily);
            helper.MinFontSize = ReadIntArgument("Min Font Size:", defaultValue: helper.MinFontSize);
            helper.MaxFontSize = ReadIntArgument("Max Font Size:", defaultValue: helper.MaxFontSize);
            helper.RandomColors =  Convert.ToBoolean(ReadIntArgument("Use Random colors?[0=false else true]:", defaultValue: 0));
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