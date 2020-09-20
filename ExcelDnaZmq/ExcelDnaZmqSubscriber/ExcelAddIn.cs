using Communications.Subscribe;
using ExcelDna.Integration;

namespace ExcelDnaZmqSubscriber
{
    public class ExcelAddIn: IExcelAddIn
    {
        private static Subscriber _subscriber;

        public void AutoOpen()
        {
            _subscriber = new Subscriber("tcp://localhost:5555");
            Functions.Subscriber = _subscriber;
        }

        public void AutoClose()
        {
            _subscriber.Stop();
        }
    }
}