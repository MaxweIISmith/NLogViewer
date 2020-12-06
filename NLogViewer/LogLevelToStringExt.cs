using System;
using NLog;

namespace NLogViewer
{
    public static class LogLevelToStringExt
    {
        public static string GetLogLevelDescription(this LogLevel level)
        {
            if (level == null)
                return String.Empty;
            if (level == LogLevel.Info)
                return "Info";
            if (level == LogLevel.Debug)
                return "Debug";
            if (level == LogLevel.Fatal)
                return "Fatal";
            if (level == LogLevel.Error)
                return "Error";
            if (level == LogLevel.Warn)
                return "Warn";
            if (level == LogLevel.Trace)
                return "Trace";

            return level.Name;
        }
    }
}