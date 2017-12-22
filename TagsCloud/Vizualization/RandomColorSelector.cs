using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Layouter;

namespace TagsCloud.Vizualization
{
    public class RandomColorSelector : IColorSelector
    {
        private readonly Random rnd;

        public RandomColorSelector() => rnd = new Random();

        public List<WordLayoutComponent> SetColorsFor(List<WordLayoutComponent> components)
        {
            foreach (var component in components)
                component.Brush = new SolidBrush(GetRandomColor());
            return components;
        }

        private Color GetRandomColor()
        {
            var r = rnd.Next(255);
            var g = rnd.Next(255);
            var b = rnd.Next(255);
            if (r + g + b > 700) return GetRandomColor();
            var color = Color.FromArgb(r, g, b);
            return color;
        }

        
    }
}