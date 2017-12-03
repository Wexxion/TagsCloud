using System;
using Autofac;
using TagCloudApplication.UI;

namespace TagCloudApplication
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var container = DiContainer.GetContainer();
            IUi ui;
            if (args.Length == 1 && args[0] == "-c")
                ui = container.Resolve<ConsoleUi>();
            else
            {
                PrintUsage();
                ui = container.Resolve<GraphicUi>();
            }
            ui.Run();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Welcome to TagCloud!");
            Console.WriteLine("Choose mode: -g | -c");
            Console.WriteLine("c - Console user interface");
            Console.WriteLine("g - Graphic user interface (DEFAULT)\n");
        }
    }
}
