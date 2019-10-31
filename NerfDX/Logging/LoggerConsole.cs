using System;
using System.Threading;

namespace NerfDX.Logging
{
    /// <summary>
    /// Basic logger writes to Console if no other logger is supplied. 
    /// </summary>
    class LoggerConsole : ILogger
    {
        private const string PREFIX_INFO = "INFO: ";
        private const string PREFIX_ERROR = "ERROR: ";
        private const string PREFIX_DEBUG = "DEBUG: ";
        private const string PREFIX_WARNING = "WARNING: ";

        public string Name { get; }

        public LoggerConsole(string name)
        {
            Name = name;
        }

        public void Debug(string message)
        {
            Console.WriteLine(FormatMessage(PREFIX_DEBUG, message));
        }

        public void Error(string message)
        {
            Console.WriteLine(FormatMessage(PREFIX_ERROR, message));
        }

        public void Warning(string message)
        {
            Console.WriteLine(FormatMessage(PREFIX_WARNING, message));
        }

        public void Error(string message, Exception e)
        {
            Error (message + e.Message + "\r\n" + e.StackTrace);
        }

        public void Info(string message)
        {
            Console.WriteLine(FormatMessage(PREFIX_INFO, message));
        }

        // 2019-10-30 17:30:19,992 [NerfDX.Connector] INFO  NerfDX.DirectInputManager | Thread started with re/connection interval = 500 MS
        private string FormatMessage(string prefix, string message)
        {
            return 
                DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss.fff' '") +
                "[" + Thread.CurrentThread.Name + "] " +
                prefix + " " +
                Name + " | " +
                message;
        }
    }
}
