using System;
using System.Drawing;
using System.IO;

namespace TagsCloud
{
    public class Program
    {
        static void Main(string[] args)
        {
            PrintUsage();
            var ui = Console.ReadLine().ToLowerInvariant();
            if (ui.Contains("cui"))
            {

            }
            else if (ui.Contains("gui"))
            {

            }
            else
            {
                PrintUsage();
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Welcome to TagCloud!");
            Console.WriteLine("Choose mode: [CUI/GUI]");
            Console.WriteLine("CUI - Console user interface");
            Console.WriteLine("GUI - Graphic user interface");
        }


        //public static void DrawWordCloud(string source, int minWordLength = 3, int topNWords = 0)
        //{
        //    var text = File.ReadAllText(source);
        //    var analyzer = new TextAnalyzer(text, minWordLength, topNWords);
        //    var viz = new TagCloudVizualizer(@"..\..\VisualizationData\WordCloud.png");
        //    var words = analyzer.GetWordsWithSizes();
        //    var center = Point.Empty;
        //    var layouter = new CircularCloudLayouter(center);
        //    foreach (var word in words)
        //        word.LayoutRectangle = layouter.PutNextRectangle(word.Size);
        //    viz.DrawTagCloud(words, center);
        //}

        //public static void DrawRandomRects(int count, string name)
        //{
        //    var center = Point.Empty;
        //    var layouter = new CircularCloudLayouter(center);
        //    foreach (var size in Extensions.GenerateRandomRectSize(count))
        //        layouter.PutNextRectangle(size);
        //    var viz = new TagCloudVizualizer($@"..\..\VisualizationData\{name}.png");
        //    viz.DrawRectCloud(layouter.Rectangles, center);
        //}
    }
}