using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloud.Infrastructure
{
    public interface ITextReader
    {
        Result<List<string>> ReadText(string source);
    }

    public class FileTextReader : ITextReader
    {
        public Result<List<string>> ReadText(string filepath) 
            => Result.Of(() => File.ReadAllLines(filepath).ToList());
    }
}