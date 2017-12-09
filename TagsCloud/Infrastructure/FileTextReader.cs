using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloud.Infrastructure
{
    public interface ITextReader
    {
        IEnumerable<string> ReadText(string source);

    }

    public class FileTextReader : ITextReader
    {
        public IEnumerable<string> ReadText(string filepath)
        {
            if (string.IsNullOrEmpty(filepath)) throw new ArgumentNullException();
            return File.ReadAllLines(filepath);
        }
    }
}