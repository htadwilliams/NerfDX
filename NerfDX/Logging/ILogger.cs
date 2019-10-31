using System;

namespace NerfDX.Logging
{
    /// <summary>
    /// Logging-system agnostic interface allowing users to wrap and plug in a logger such as Log4net or NLog.
    /// See Example project JoyLog for use.
    /// </summary>
    public interface ILogger
    {
        string Name
        {
            get;
        }

        void Info(string message);
        void Debug(string message);
        void Warning(string message);
        void Error(string message);
        void Error(string message, Exception e);
    }
}
