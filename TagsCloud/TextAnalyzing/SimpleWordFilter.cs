using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloud.Interfaces;
using TagsCloud.TextAnalyzing.Interfaces;

namespace TagsCloud.TextAnalyzing
{
    public class SimpleWordFilter : IWordFilter
    {

        private HashSet<string> boringWords;
        public ITextReader TextReader { get; }

        public SimpleWordFilter(ITextReader textReader) => TextReader = textReader;

        public IEnumerable<string> FilterWords(IEnumerable<string> words)
        {
            boringWords = new HashSet<string>();
            if (TextReader.Filepath != null)
                foreach (var line in TextReader.ReadText())
                    boringWords.Add(line);
            return words.Where(word => !boringWords.Contains(word) && word.Length > 3);
        }
    }
}