using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.Infrastructure
{
    public static class Extensions
    {
        public static bool IntersectsWith(this Rectangle rect, List<Rectangle> rectangles)
        {
            for (var i = rectangles.Count - 1; i >= 0; i--)
                if (rect.IntersectsWith(rectangles[i]))
                    return true;
            return false;
        }

        public static List<T> ReferenceClone<T>(this IReadOnlyCollection<T> listToClone) 
            => listToClone.Select(item => item).ToList();

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            var res = new HashSet<T>();
            foreach (var item in collection)
                res.Add(item);
            return res;
        }
    }
}
