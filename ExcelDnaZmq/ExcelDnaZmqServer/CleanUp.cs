using System;
using Communications.Publish;
using ExcelDna.Integration;

namespace ExcelDnaZmqServer
{
    public class CleanUp: ExcelComAddIn
    {
        private Publisher _publisher;

        public CleanUp(Publisher publisher)
        {
            _publisher = publisher;
        }
        public override void OnBeginShutdown(ref Array custom)
        {
            base.OnBeginShutdown(ref custom);
            //Cleanup the zmq resources
            _publisher.Stop();
        }
    }
}