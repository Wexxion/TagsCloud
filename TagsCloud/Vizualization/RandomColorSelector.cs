using System;
using System.Drawing;
using TagsCloud.Layouter;

namespace TagsCloud.Vizualization
{
    public class RandomColorSelector : IColorSelector
    {
        private readonly Random rnd;
        public RandomColorSelector() => rnd = new Random();


        public void SetColorFor<T>(ILayoutComponent<T> component) 
            => component.Brush = new SolidBrush(GetRandomColor());
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