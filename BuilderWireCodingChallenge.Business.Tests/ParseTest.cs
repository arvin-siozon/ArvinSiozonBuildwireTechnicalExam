using System.Collections.Generic;
using System.Linq;
using BuilderWireCodingChallenge.Business.Interfaces;
using BuilderWireCodingChallenge.Business.Services;
using BuilderWireCodingChallenge.Domain.Entities;
using Xunit;
using Xunit.Abstractions;

namespace BuilderWireCodingChallenge.Business.Tests
{
    public class ParseTest
    {
        private readonly ITestOutputHelper output;
        private ILogger _logger;
        private IParser _parser;
        public ParseTest(ITestOutputHelper output)
        {
            this.output = output;
        }
        #region Article Input

        private readonly string input =
            "this is what i learned from mr| jones about a paragraph. a paragraph is a group of words put together to form a group that is usually longer than a sentence.";
        #endregion
        #region Expected Words For Working Test

        private readonly List<string>  sentenceOneWords = new List<string>
        {
            "this",
            "is",
            "what",
            "i",
            "learned",
            "from",
            "mr.",
            "jones",
            "about",
            "a",
            "paragraph"
        };

        private readonly List<string> sentenceTwoWords = new List<string>
        {
            "group",
            "of",
            "words",
            "put",
            "together",
            "to",
            "form",
            "that",
            "usually",
            "longer",
            "than",
            "sentence"
        };

        #endregion
        #region Expected Words For Failed Test

        private readonly List<string> sentenceOneWords1 = new List<string>
        {
            "this",
            "is",
            "what",
            "i",
            "learned",
            "from",
            "mr.",
            "jones",
            "about",
            "a",
            "paragraph",
            "testWordDoesNotExistsFromInput1"
        };

        private readonly List<string> sentenceTwoWords2 = new List<string>
        {
            "group",
            "of",
            "words",
            "put",
            "together",
            "to",
            "form",
            "that",
            "usually",
            "longer",
            "than",
            "sentence",
            "testWordDoesNotExistsFromInput2"
        };

        #endregion
        [Fact]
        public void InputParser_CheckIfSentenceListIsNotEmpty_ShouldWork()
        {
            #region Add words to generic type Sentence

            var sentenceList = new List<Sentence>
            {
                new Sentence(sentenceOneWords, 1)
            };

            sentenceList.Add(new Sentence(sentenceTwoWords, 1));

            #endregion
            _logger = new Logger();
            _parser = new Parser(_logger);
            var sentences = _parser.InputParser(input);

            Assert.True(sentences.Any());
            output.WriteLine("Sentence list is not empty.");
        }
        [Fact]
        public void InputParser_CheckIfSentenceListIfMatchesTheExpectedResult_ShouldWork()
        {
            #region Add words to generic type Sentence

            var sentenceList = new List<Sentence>();

            sentenceList.Add(new Sentence(sentenceOneWords, 1));
            sentenceList.Add(new Sentence(sentenceTwoWords, 2));

            #endregion
            _logger = new Logger();
            _parser = new Parser(_logger);
            var sentences = _parser.InputParser(input);
            var status = true;
            var isExists = false;
            foreach (var sentence in sentenceList)
            {
                foreach (var word in sentence.Words)
                {
                    foreach (var s in sentences)
                    {
                        isExists = s.Words.Contains(word);
                        if (isExists)
                        {
                            break;
                        }
                    }

                    if (!isExists)
                    {
                        output.WriteLine($"The word '{word}' does not exists.");
                        status = false;
                    }
                }

            }
            Assert.True(status);
            output.WriteLine($"All words exists.");
        }

        [Fact]
        public void InputParser_CheckIfSentenceListIfMatchesTheExpectedResult_ShouldNotWork()
        {

            #region Article Input

            string input =
                "this is what i learned from mr| jones about a paragraph. a paragraph is a group of words put together to form a group that is usually longer than a sentence.";
            #endregion
          

            #region Add words to generic type Sentence

            var sentenceList = new List<Sentence>();
            
            sentenceList.Add(new Sentence(sentenceOneWords1, 1));
            sentenceList.Add(new Sentence(sentenceTwoWords2, 2));

            #endregion
            _logger = new Logger();
            _parser = new Parser(_logger);
            var sentences = _parser.InputParser(input);
            var status = true;
            var isExists = false;
            foreach (var sentence in sentenceList)
            {
                foreach (var word in sentence.Words)
                {
                    foreach (var s in sentences)
                    {
                        isExists = s.Words.Contains(word);
                        if (isExists)
                        {
                            break;
                        }
                    }

                    if (!isExists)
                    {
                        output.WriteLine($"The word '{word}' does not exists.");
                        status = false;
                    }
                }

            }
            Assert.True(status);
        }

        [Fact]
        public void ReplacePipeCharacterWithPeriod_ShouldWork()
        {
            _logger = new Logger();
            _parser = new Parser(_logger);
            var sentences = new[]
            {
                "this is what i learned from mr| jones about a paragraph",
                " a paragraph is a group of words put together to form a group that is usually longer than a sentence"
            };
            var sentenceList = _parser.SentenceParser(sentences);

            var testStatus = true;
            if (sentenceList.Any())
            {
                output.WriteLine("Successful on executung SentenceParser method: 'sentenceList' is not empty");
            }
            else
            {
                output.WriteLine("Failed on executung SentenceParser method: 'sentenceList' is empty");
                testStatus = false;
            }

            foreach (var word in from sentence in sentenceList from word in sentence.Words let checkIfPipeCharacterExists = word.Contains("|") where checkIfPipeCharacterExists select word)
            {
                output.WriteLine($"Pipe character exists in this word: [{word}]");
                testStatus = false;
            }
            Assert.True(testStatus);

        }

        [Fact]
        public void ReplacePipeCharacterWithPeriod_ShouldNotWork()
        {
            _logger = new Logger();
            _parser = new Parser(_logger);
            var sentences = new[]
            {
                "this is what i learned from mr| jones about a paragraph",
                " a paragraph is a group of words put together to form a group that is usually longer than a sentence"
            };
            var sentenceList = _parser.SentenceParser(sentences);
            //add pipe character intentionally to fail the result
            var wordList = new List<string>();
            wordList.Add("TestWordWithPipe_One|");
            wordList.Add("TestWordWithPipe_Two|");
            sentenceList.Add(new Sentence(wordList, 3));
            var testStatus = true;
            if (sentenceList.Any())
            {
                output.WriteLine("Successful on executung SentenceParser method: 'sentenceList' is not empty");
            }
            else
            {
                output.WriteLine("Failed on executung SentenceParser method: 'sentenceList' is empty");
                testStatus = false;
            }

            foreach (var word in from sentence in sentenceList from word in sentence.Words let checkIfPipeCharacterExists = word.Contains("|") where checkIfPipeCharacterExists select word)
            {
                output.WriteLine($"Pipe character exists in this word: [{word}]");
                testStatus = false;
            }
            Assert.True(testStatus);

        }


    }

}
