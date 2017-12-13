using System.Drawing;
using System.IO;
using Autofac;
using TagCloudApplication.UI;
using TagsCloud;
using TagsCloud.Infrastructure;

namespace TagCloudApplication
{
    public static class DiContainer
    {
        public static IContainer GetContainer()
        {
            var cwd = Directory.GetParent(Directory.GetCurrentDirectory()).Parent?.Parent?.FullName + "\\";
            var boringWords = File.ReadLines(cwd + @"Data\boring_words.txt").ToHashSet(); 
            var center = Point.Empty;
            var builder = new ContainerBuilder();
            builder
                .RegisterAssemblyTypes(typeof(TagCloudSettings).Assembly)
                .WithParameter("boringWords", boringWords)
                .WithParameter("center", center)
                .AsSelf()
                .AsImplementedInterfaces();
            builder.RegisterType<TagCloudHelper>().AsSelf();
            builder.RegisterType<ConsoleUi>().AsSelf();
            builder.RegisterType<GraphicUi>().AsSelf();
            return builder.Build();

        }
    }


}