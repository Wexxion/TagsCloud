using System;
using System.Collections.Generic;
using TagsCloud.Interfaces;
using TagsCloud.Layouter;
using TagsCloud.Layouter.Interfaces;
using TagsCloud.TextAnalyzing;
using TagsCloud.TextAnalyzing.Interfaces;

namespace TagsCloud
{
    public class ConsoleUi : IUi
    {
        private readonly ITextReader textReader;
        private readonly ITagCloudLayouter layouter;
        private readonly IFontAnalyzer fontAnalyzer;
        private readonly ITextAnalyzer textAnalyzer;
        private readonly TagCloudVizualizer vizualizer;

        public ConsoleUi(ITextReader textReader, ITagCloudLayouter layouter, 
            IFontAnalyzer fontAnalyzer, ITextAnalyzer textAnalyzer, TagCloudVizualizer vizualizer)
        {
            this.textReader = textReader;
            this.layouter = layouter;
            this.fontAnalyzer = fontAnalyzer;
            this.textAnalyzer = textAnalyzer;
            this.vizualizer = vizualizer;
        }

        public void Run()
        {
            ReadArguments();

            var text = textReader.ReadText();
            var words = textAnalyzer.GetSortedWords(text);
            words = fontAnalyzer.SetFontForWords(words);
            var components = new List<ILayoutComponent<Word>>();
            foreach (var word in words)
            {
                var component = new WordLayoutComponent(word);
                component.LayoutRectangle = layouter.PutNextRectangle(component.Size);
                components.Add(component);
            }
            vizualizer.DrawTagCloud(components);
        }

        private void ReadArguments()
        {
            textReader.Filepath = ReadStringArgument("Path to file with text:", defaultValue: null);
            vizualizer.FilePath = ReadStringArgument("Save to (with name):", defaultValue: null);
            textAnalyzer.TopNWords = ReadIntArgument("Use top N words (0 - all):", defaultValue: 0);
            textAnalyzer.MinWordLength = ReadIntArgument("Min word length:", defaultValue: 3);
            fontAnalyzer.FontFamily = ReadStringArgument("Font", defaultValue: "Calibri");
            fontAnalyzer.MinFontSize = ReadIntArgument("Min Font Size:", defaultValue: 20);
            fontAnalyzer.MaxFontSize = ReadIntArgument("Max Font Size:", defaultValue: 72);
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
            Console.WriteLine(msg);
            var arg = Console.ReadLine();
            if (string.IsNullOrEmpty(arg)) return defaultValue;
            if (int.TryParse(arg, out var result))
                return result;
            throw new FormatException("Cant Parse Your Argument! Try again");
        }
    }
}