using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderWireCodingChallenge.Business.Interfaces
{
    public interface ITextProcessor
    {
        string PrepareData(string file1, string file2);
        StringBuilder FileReader(string file);
        List<string> TextToList(string path);
        void TextWriter(List<string> input, string path, bool isAppendMode);
    }
}
