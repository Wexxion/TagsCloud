using System.Collections.Generic;

namespace TagsCloud.Interfaces
{
    public interface ITextReader
    {
        IEnumerable<string> ReadText();
        string Filepath { get; set; }
    }
}
