﻿using Autofac;
using TagsCloud.Infrastructure;
using TagsCloud.Interfaces;
using TagsCloud.Layouter;
using TagsCloud.Layouter.Interfaces;
using TagsCloud.TextAnalyzing;
using TagsCloud.TextAnalyzing.Interfaces;
using TagsCloud.Vizualization;

namespace TagsCloud
{
    public static class DiContainer
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FileTextReader>().As<ITextReader>();
            builder.RegisterType<TagCloud>().AsSelf();
            builder.RegisterType<CircularCloudLayouter>().As<ITagCloudLayouter>();
            builder.RegisterType<FontAnalyzer>().As<IFontAnalyzer>();
            builder.RegisterType<TextAnalyzer>().As<ITextAnalyzer>();
            builder.RegisterType<SimpleWordFilter>().As<IWordFilter>();
            builder.RegisterType<SimpleWordConverter>().As<IWordConverter>();
            builder.RegisterType<TagCloudVizualizer>().AsSelf();
            builder.RegisterType<ImageConfigurator>().As<IImageConfigurator>();
            builder.RegisterType<FileImageSaver>().As<IImagaSaver>();
            builder.RegisterType<PointFactory>().AsSelf();
            builder.RegisterType<ConsoleUi>().AsSelf();
            return builder.Build();
        }
    }
}