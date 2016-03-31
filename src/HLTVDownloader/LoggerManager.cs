using System;
using Pagansoft.Logging;

namespace PaganSoft.HLTVDownloader
{
    public static class LoggerManager
    {
        private static ILogger _currentLogger;

        private static void EnsureLogger()
        {
            if (_currentLogger == null)
                _currentLogger = Bootstrapper.GetExport<ILogger>();
        }

        public static void Trace(string message, params object[] args)
        {
            EnsureLogger();
            _currentLogger.LogTrace(message, args);
        }

        public static void Debug(string message, params object[] args)
        {
            EnsureLogger();
            _currentLogger.LogDebug(message, args);
        }

        public static void Info(string message, params object[] args)
        {
            EnsureLogger();
            _currentLogger.LogInfo(message, args);
        }

        public static void Warn(string message, params object[] args)
        {
            EnsureLogger();
            _currentLogger.LogWarn(message, args);
        }

        public static void Error(string message, params object[] args)
        {
            EnsureLogger();
            _currentLogger.LogError(message, args);
        }

        public static void Error(Exception ex, string message, params object[] args)
        {
            EnsureLogger();
            _currentLogger.LogError(ex, message, args);
        }

        public static void Fatal(string message, params object[] args)
        {
            EnsureLogger();
            _currentLogger.LogFatal(message, args);
        }

        public static void Fatal(Exception ex, string message, params object[] args)
        {
            EnsureLogger();
            _currentLogger.LogFatal(ex, message, args);
        }
    }
}