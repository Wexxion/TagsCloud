using System;
using System.Drawing;
using Autofac;
using TagCloudApplication.UI;
using TagsCloud;
using TagsCloud.Infrastructure;
using TagsCloud.Layouter;
using TagsCloud.TextAnalyzing;
using TagsCloud.Vizualization;

namespace TagCloudApplication
{
    public static class DiContainer
    {
        public static IContainer GetContainer()
        {
            var center = Point.Empty;
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(TagCloud).Assembly).AsSelf().AsImplementedInterfaces();
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloudLayouter>().WithParameter("center", center);
            builder.RegisterType<TagCloudVizualizer>().AsSelf().WithParameter("center", center);
            builder.RegisterType<ConsoleUi>().AsSelf();
            builder.RegisterType<GraphicUi>().AsSelf();
            return builder.Build();
        }
    }
}