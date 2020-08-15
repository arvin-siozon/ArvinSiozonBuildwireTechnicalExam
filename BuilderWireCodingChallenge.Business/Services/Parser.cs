using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BuilderWireCodingChallenge.Business.Interfaces;
using BuilderWireCodingChallenge.Domain.Entities;

namespace BuilderWireCodingChallenge.Business.Services
{
    public class Parser : IParser
    {
        private readonly char[] _sentenceSeparators = { '.', '?', '!' };
        private readonly string _replaceCharactersNotAllowedInWordsWithSpaces = ConfigurationManager.AppSettings["RegExReplaceCharactersNotAllowedInWordsWithSpaces"];
        private ILogger _logger;

        public Parser(ILogger logger)
        {
            _logger = logger;
        }
        public List<Sentence> InputParser(string input)
        {
            var sentenceList = new List<Sentence>();
            try
            {
                var sentences = input.Split(_sentenceSeparators, StringSplitOptions.RemoveEmptyEntries);


                sentenceList = SentenceParser(sentences);

                //
            }
            //just showing on how to manage different type of exceptions even though it would be useless for this coding challenge
            catch (ArgumentNullException ex)
            {
                //code specifically for a ArgumentNullException
                _logger.LogException(ex);
            }
            catch (ArgumentException ex)
            {
                //code specifically for a ArgumentException
                _logger.LogException(ex);
            }
            catch (WebException ex)
            {
                //code specifically for a WebException
                _logger.LogException(ex);
            }
            catch (Exception ex)
            {
                //code for any other type of exception
                _logger.LogException(ex);
            }

            return sentenceList;
        }

        public List<Sentence> SentenceParser(string[] sentences)
        {
            var sentenceList = new List<Sentence>();
            var sentenceCounter = 0;
            foreach (var sentence in sentences)
            {
                sentenceCounter++;

                var words = ReplacePipeCharacterWithPeriod(ReplaceCharactersNotAllowedInWordsWithSpaces(sentence))
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();


                if (words.Any())
                {
                    sentenceList.Add(new Sentence(words, sentenceCounter));
                }
            }

            return sentenceList;
        }
        private List<string> RepopulateWithAlphabet(List<string> dataList)
        {
            var index = 0;
            var multiplier = 1;
            var output = new List<string>();
            string alphabet = "";

            try
            {
                foreach (var item in dataList)
                {
                    var alpha = "";
                    if (index > 25)
                    {
                        index = 0;
                        multiplier++;
                        alphabet = alpha;
                    }

                    alpha = alphabet + "" + GenerateAlphabet(multiplier, index);
                    index++;

                    output.Add($" {alpha}. {item}");
                }
            }
            //just showing on how to manage different type of exceptions even though it would be useless for this coding challenge
            catch (ArgumentNullException ex)
            {
                //code specifically for a ArgumentNullException
                _logger.LogArgumentNullException(ex);
            }
            catch (ArgumentException ex)
            {
                //code specifically for a ArgumentException
                _logger.LogArgumentException(ex);
            }
            catch (WebException ex)
            {
                //code specifically for a WebException
                _logger.LogWebException(ex);
            }
            catch (Exception ex)
            {
                //code for any other type of exception
                _logger.LogException(ex);
            }
            return output;
        }
        private string GenerateAlphabet(int counter, int index)
        {
            var alphabet = "";
            string[] alphabets =
            {
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u",
                "v", "w", "x", "y", "z"
            };
            try
            {
                for (int i = 0; i < counter; i++)
                {
                    alphabet = alphabet + alphabets[index];
                }
            }
            //just showing on how to manage different type of exceptions even though it would be useless for this coding challenge
            catch (ArgumentNullException ex)
            {
                //code specifically for a ArgumentNullException
                _logger.LogArgumentNullException(ex);
            }
            catch (ArgumentException ex)
            {
                //code specifically for a ArgumentException
                _logger.LogArgumentException(ex);
            }
            catch (WebException ex)
            {
                //code specifically for a WebException
                _logger.LogWebException(ex);
            }
            catch (Exception ex)
            {
                //code for any other type of exception
                _logger.LogException(ex);
            }
            return alphabet;
        }
        protected string ReplacePipeCharacterWithPeriod(string s)
        {
            return s.Replace("|", ".");
        }
        private string ReplaceCharactersNotAllowedInWordsWithSpaces(string s)
        {
            return Regex.Replace(s, _replaceCharactersNotAllowedInWordsWithSpaces, " ");
        }
        public List<string> ProcessOutput(List<Sentence> sentenceList, List<string> refWords)
        {
            var output = new List<string>();
            var finalOutput = new List<string>();
            var totalWords = 0;
            try
            {
                foreach (var rf in refWords)
                {
                    var wordInfo = "";
                    var position = "";
                    var count = 0;
                    foreach (var sentence in sentenceList)
                    {

                        var pos = "";
                        foreach (var w in sentence.Words)
                        {
                            if (w == rf)
                            {
                                count++;
                                wordInfo = $"{rf }" + " {" + $"{count}:";
                                if (position.Any())
                                {
                                    position = $"{position},{sentence.Position}";
                                }
                                else
                                {
                                    position = $"{sentence.Position}";
                                }

                            }
                        }

                    }

                    var final = wordInfo + $"{position}" + "}";
                    output.Add(final);

                }
            }
            //just showing on how to manage different type of exceptions even though it would be useless for this coding challenge
            catch (ArgumentNullException ex)
            {
                //code specifically for a ArgumentNullException
                _logger.LogArgumentNullException(ex);
            }
            catch (ArgumentException ex)
            {
                //code specifically for a ArgumentException
                _logger.LogArgumentException(ex);
            }
            catch (WebException ex)
            {
                //code specifically for a WebException
                _logger.LogWebException(ex);
            }
            catch (Exception ex)
            {
                //code for any other type of exception
                _logger.LogException(ex);
            }

            return RepopulateWithAlphabet(output);
        }

    }
}
