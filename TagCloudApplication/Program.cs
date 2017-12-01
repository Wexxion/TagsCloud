using System;
using Autofac;

namespace TagCloudApplication
{
    class Program
    {
        [STAThread]
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
                var gui = container.Resolve<GraphicUi>();
                gui.Run();
            }
            else
            {
                Console.WriteLine("Try again =[");
                PrintUsage();
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
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
