using System;
using TagCloudGui;

namespace TagCloudApplication
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 1 when args[0] == "-c":
                    new ConsoleUi().Run();
                    break;
                case 1 when args[0] == "-g":
                    var app = new App();
                    app.Run(new TagCloudWindow());
                    break;
                default:
                    PrintUsage();
                    break;
            }
            new ConsoleUi().Run();
            //var application = new App();
            //application.Run(new TagCloudWindow());
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
