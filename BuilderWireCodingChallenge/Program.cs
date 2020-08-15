using System;
using System.Configuration;

using BuilderWireCodingChallenge.Business.Interfaces;

using BuilderWireCodingChallenge.Business.Services;

namespace BuilderWireCodingChallenge
{
    class Program
    {

        static void Main(string[] args)
        {
            ILogger logger = new Logger();
            IParser parser = new Parser(logger);
            ITextProcessor iTextProcessor = new TextProcessor(logger);
            
            string file1 = @"E:\Tech Exam\Input\Article.txt";
            string file2 = @"E:\Tech Exam\Input\Words.txt";
            var userInterface = new UserInterface(parser, iTextProcessor);
            userInterface.ProcessTextFiles(file1, file2);

        }
    }

    //assuming this is the user interface that accepts input and return output
    class UserInterface
    {
        private ITextProcessor _iTextProcessor;
        private IParser _parser;
        private readonly string _outputPath = ConfigurationManager.AppSettings["OutputPath"];
        public UserInterface(IParser parser, ITextProcessor iTextProcessor)
        {
            _parser = parser;
            _iTextProcessor = iTextProcessor;
        }
        public void ProcessTextFiles(string file1, string file2)
        {
            var input = _iTextProcessor.PrepareData(file1, file2);
            var refWords = _iTextProcessor.TextToList(file2);
           
            var sentences = _parser.InputParser(input);
            var output = _parser.ProcessOutput(sentences, refWords);
            _iTextProcessor.TextWriter(output, _outputPath, true);
            Console.WriteLine();

            Console.WriteLine("Output------------------------------");
            foreach (var word in output)
            {
                Console.WriteLine(word);
               
            }
            
            Console.ReadLine();
        }
    }
}
