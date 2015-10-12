using System;

namespace Pagansoft.Logging
{
    public static class LoggerExtensions
    {
        public static void LogTrace(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
                return;
            try
            {
                logger.Trace(message, args);
            }
            catch
            {
                /* intentionally left blank */
            }
        }

        public static void LogDebug(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
                return;
            try
            {
                logger.Debug(message, args);
            }
            catch
            {
                /* intentionally left blank */
            }
        }

        public static void LogInfo(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
                return;
            try
            {
                logger.Info(message, args);
            }
            catch
            {
                /* intentionally left blank */
            }
        }

        public static void LogWarn(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
                return;
            try
            {
                logger.Warn(message, args);
            }
            catch
            {
                /* intentionally left blank */
            }
        }

        public static void LogError(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
                return;
            try
            {
                logger.Error(message, args);
            }
            catch
            {
                /* intentionally left blank */
            }
        }

        public static void LogError(this ILogger logger, Exception ex, string message, params object[] args)
        {
            if (logger == null)
                return;
            try
            {
                logger.Error(ex, message, args);
            }
            catch
            {
                /* intentionally left blank */
            }
        }

        public static void LogFatal(this ILogger logger, string message, params object[] args)
        {
            if (logger == null)
                return;
            try
            {
                logger.Fatal(message, args);
            }
            catch
            {
                /* intentionally left blank */
            }
        }

        public static void LogFatal(this ILogger logger, Exception ex, string message, params object[] args)
        {
            if (logger == null)
                return;
            try
            {
                logger.Fatal(ex, message, args);
            }
            catch
            {
                /* intentionally left blank */
            }
        }
    }
}

