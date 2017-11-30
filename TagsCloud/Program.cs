using System;
using Autofac;
namespace TagsCloud
{
    public class Program
    {
        static void Main(string[] args)
        {
            PrintUsage();
            var container = DiContainer.GetContainer();
            var ui = Console.ReadLine().ToLowerInvariant();
            if (ui.Contains("cui"))
            {
                var cui = container.Resolve<ConsoleUi>();
                cui.Run();
            }
            else if (ui.Contains("gui"))
            {
                throw new NotImplementedException();
            }
            else
            {
                Console.WriteLine("Try again =[");
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
            Console.WriteLine("GUI - Graphic user interface\n");
        }
    }
}