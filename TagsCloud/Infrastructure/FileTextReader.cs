using System;
using System.Collections.Generic;
using System.IO;
using TagsCloud.Interfaces;

namespace TagsCloud.Infrastructure
{
    public class FileTextReader : ITextReader
    {
        public string Filepath { get; set; }

        public IEnumerable<string> ReadText()
        {
            if (string.IsNullOrEmpty(Filepath)) throw new ArgumentNullException();
            return File.ReadAllLines(Filepath);
        }
    }
}