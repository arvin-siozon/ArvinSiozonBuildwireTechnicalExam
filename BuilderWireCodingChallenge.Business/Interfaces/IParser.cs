using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuilderWireCodingChallenge.Domain.Entities;

namespace BuilderWireCodingChallenge.Business.Interfaces
{
    public interface IParser
    {
        List<Sentence> InputParser(string input);
        List<Sentence> SentenceParser(string[] sentences);
        List<string> ProcessOutput(List<Sentence> sentenceList, List<string> refWords);
    }
}
