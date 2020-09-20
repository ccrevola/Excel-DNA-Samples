using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communications.Publish;
using ExcelDna.Integration;

namespace ExcelDnaZmqServer
{
    public class ExcelAddIn: IExcelAddIn
    {
        private Publisher _publisher;

        public void AutoOpen()
        {
            Logger.Configure();
            Trace.WriteLine("Trace should be configured now", "Debug");
            _publisher = new Publisher();
            _publisher.Run();
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            var cleanup = new CleanUp(_publisher);
            ExcelComAddInHelper.LoadComAddIn(cleanup);
            ExcelIntegration.RegisterUnhandledExceptionHandler(OnAddinException);
            PublisherFunctions.Publisher = _publisher;
        }

        public void AutoClose()
        {
            AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            ReportException((Exception)args.ExceptionObject);
        }

        private static object OnAddinException(object exceptionObject)
        {
            var exception = exceptionObject as Exception;
            if (exception != null)
                ReportException(exception);
            return exceptionObject;
        }

        private static void ReportException(Exception exception)
        {
            Trace.WriteLine(exception);
        }
    }
}
