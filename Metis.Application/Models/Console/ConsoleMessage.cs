using System;
using System.Globalization;

namespace Metis.Application.Models
{
    internal struct ConsoleMessage
    {
        public ConsoleMessage(string source, string message, string time)
        {
            Source = source;
            Message = message;
            Time = time;
        }

        public ConsoleMessage(string source, string message, DateTime time)
            : this(source, message, time.ToString("HH:mm:ss", CultureInfo.InvariantCulture))
        {
        }

        public ConsoleMessage(string source, string message)
            : this(source, message, DateTime.Now)
        {
        }

        public string Source { get; }

        public string Message { get; }

        public string Time { get; }
    }
}
