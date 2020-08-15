using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderWireCodingChallenge.Domain.Entities
{
    public class Sentence
    {
        public List<string> Words { get; set; }
        public int Position { get; set; }

        public Sentence(List<string> words, int position)
        {
            Words = words;
            Position = position;
        }
    }
}
