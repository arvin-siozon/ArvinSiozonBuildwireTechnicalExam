using System;

using System.Net;
namespace BuilderWireCodingChallenge.Business.Interfaces
{
    public interface ILogger
    {
        void LogException(Exception exception);
        void LogArgumentException(ArgumentException exception);
        void LogArgumentNullException(ArgumentNullException exception);
        void LogWebException(WebException exception);


    }
}
