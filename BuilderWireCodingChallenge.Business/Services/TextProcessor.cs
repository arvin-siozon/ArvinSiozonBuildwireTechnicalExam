using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BuilderWireCodingChallenge.Business.Interfaces;

namespace BuilderWireCodingChallenge.Business.Services
{
    public class TextProcessor : ITextProcessor
    {
        private ILogger _logger;
        public StringBuilder FileReader(string file1)
        {
            var stringBuilder = new StringBuilder();
            try
            {
                using (var r = new StreamReader(file1))
                {
                    // Read line by line  
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        stringBuilder.Append(line.ToLower());
                    }
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

            return stringBuilder;
        }
        public TextProcessor(ILogger logger)
        {
            _logger = logger;
        }

        #region Public Method
        public string PrepareData(string file1, string file2)
        {
            var readerData = FileReader(file1);
            var words = WordsToExcludeInSentenceSplitter(file2);
            var stringBuilder = ReplaceDotWithPipe(words, readerData);
            return stringBuilder.ToString();
        }
        public void TextWriter(List<string> input, string path, bool isAppendMode)
        {
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                var sw = new StreamWriter(path, false);
                //Write a line of text
                foreach (var i in input)
                {
                    sw.WriteLine(i);
                }
                sw.Close();
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
        }
        public List<string> TextToList(string path)
        {
            var wordList = new List<string>();
            try
            {
                using (var r = new StreamReader(path))
                {
                    // Read line by line  
                    string line;

                    while ((line = r.ReadLine()) != null)
                    {
                        wordList.Add(line);

                    }
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

            return wordList;
        }

        #endregion

        #region Private/Protected Methods

        private StringBuilder ReplaceDotWithPipe(IEnumerable<string> words, StringBuilder stringBuilder)
        {
            try
            {
                foreach (var word in words)
                {
                    stringBuilder.Replace(word, word.Replace(".", "|"));
                }
            }

            //just showing on how to manage different type of exceptions even though it would be useless for this coding challenge
            catch (ArgumentNullException ex)
            {
                //code specifically for a ArgumentNullException
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

            return stringBuilder;
        }
        //This method is overridable for possible modification in the future if the requirements has change into more complex scenarios
        protected virtual IEnumerable<string> WordsToExcludeInSentenceSplitter(string path)
        {
            var wordList = new List<string>();
            try
            {
                using (var r = new StreamReader(path))
                {
                    // Read line by line  
                    string line;

                    while ((line = r.ReadLine()) != null)
                    {
                        if (line.EndsWith(".", System.StringComparison.CurrentCultureIgnoreCase))
                        {
                            wordList.Add(line);
                        }

                    }
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
            return wordList;
        }


        #endregion



    }
}
