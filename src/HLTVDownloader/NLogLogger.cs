using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Xml;
using NLog;
using NLog.Config;

namespace PaganSoft.HLTVDownloader
{
    [Export(typeof(Pagansoft.Logging.ILogger))]
    public class NLogLogger : Pagansoft.Logging.ILogger
    {
        private const string NLogConfig =
            @"<?xml version='1.0' encoding='utf-8' ?>
<nlog xmlns=""http://www.nlog-project.org/schemas/NLog.xsd"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <targets async=""true"">
    <target name=""logfile""
            xsi:type=""File""
            layout="" ${longdate}|${level:uppercase=true}|${logger}|${message}""
            fileName=""${specialfolder:dir=.hltc:file=hltvdownloader.log:folder=Personal}""
            lineEnding=""LF""
            replaceFileContentsOnEachWrite=""true"" />
    <target name=""console""
            xsi:type=""Console""
            layout="" ${longdate}|${level:uppercase=true}|${logger}|${message}""  />
  </targets>

  <rules>
    <logger name=""*"" minLevel=""Error"" writeTo=""logfile"" />
    <logger name=""*"" minLevel=""Info"" writeTo=""console"" />
  </rules>
</nlog>";

        private readonly ILogger _logger;

        [ImportingConstructor]
        public NLogLogger(string className)
        {
            ConfigureLogger();
            _logger = LogManager.GetLogger(className);
        }

        private static void ConfigureLogger()
        {
            var configPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".hltc",
                "nlog.config");

            var xml = NLogConfig;
            if (File.Exists(configPath))
                xml = File.ReadAllText(configPath);
            else
                File.WriteAllText(configPath, NLogConfig);

            using (var sr = new StringReader(xml))
            using (var xr = XmlReader.Create(sr))
            {
                LogManager.Configuration = new XmlLoggingConfiguration(xr, null);
            }
        }

        public void Trace(string message, params object[] args)
        {
            _logger.Trace(message, args);
        }

        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        public void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        public void Error(Exception ex, string message, params object[] args)
        {
            _logger.Error(ex, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        public void Fatal(Exception ex, string message, params object[] args)
        {
            _logger.Fatal(ex, message, args);
        }
    }
}

