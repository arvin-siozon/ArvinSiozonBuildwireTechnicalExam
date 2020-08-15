using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BuilderWireCodingChallenge.Business.Interfaces;

namespace BuilderWireCodingChallenge.Business.Services
{
    //created a simple logging mechanism for this coding challenge
    public class Logger : ILogger
    {
        private readonly string _loggerPath = ConfigurationManager.AppSettings["LoggerPath"];
        public void LogException(Exception exception)
        {
            LogWriter(exception.Message, _loggerPath);

        }

        public void LogArgumentException(ArgumentException exception)
        {
            LogWriter(exception.Message, _loggerPath);
        }

        public void LogArgumentNullException(ArgumentNullException exception)
        {
            LogWriter(exception.Message, _loggerPath);

        }

        public void LogWebException(WebException exception)
        {
            LogWriter(exception.Message, _loggerPath);
        }

        private void LogWriter(string errorDetails, string path )
        {
            try
            {
                var logName = $"{path}\\{DateTime.Today.ToShortDateString().Replace("/","")}-{DateTime.Today.ToShortTimeString().Replace(":","")}.log";
                using (var sw = new StreamWriter(logName, true))
                {
                    sw.WriteLine($"{DateTime.Now.ToLongTimeString()}: ");
                    sw.WriteLine(errorDetails);
                }
            }
            catch 
            {
                var exception = new ArgumentException("Error when trying to create log.");
                LogWriter(exception.Message, path);
            }
        }
    }
}
