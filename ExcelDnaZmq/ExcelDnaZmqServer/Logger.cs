using System;
using System.Diagnostics;
using System.IO;
using Serilog;
using Serilog.Exceptions;

namespace ExcelDnaZmqServer
{
    public class Logger
    {
        private static bool _isConfigured;
        /// <summary>
        /// Configures the global shared logger if not set already
        /// </summary>
        public static void Configure()
        {
            //Perhaps check the instancing first
            if (!_isConfigured)
            {
                var file = Path.Combine(GetFilePath(), $"ExcelDNAZMQServer.Excel_{DateTime.Now:yyy-MM-dd}.txt");
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Enrich.WithExceptionDetails()
                    .WriteTo.File(file)
                    .CreateLogger();
                //Setup the trace listener
                var listener = new SerilogTraceListener.SerilogTraceListener();
                Trace.Listeners.Add(listener);
                _isConfigured = true;
            }
        }

        private static string GetFilePath()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "UltraSoftware",
                "ExcelDNAZMQ");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}