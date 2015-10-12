﻿using System;

namespace Pagansoft.Logging
{
    public interface ILogger
    {
        void Trace(string message, params object[] args);

        void Debug(string message, params object[] args);

        void Info(string message, params object[] args);

        void Warn(string message, params object[] args);

        void Error(string message, params object[] args);

        void Error(Exception ex, string message, params object[] args);

        void Fatal(string message, params object[] args);

        void Fatal(Exception ex, string message, params object[] args);
    }
}

