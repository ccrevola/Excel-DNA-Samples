using System.Collections.Concurrent;
using System.Collections.Generic;
using Communications.Messaging;
using Communications.Subscribe;
using ExcelDna.Integration;

namespace ExcelDnaZmqSubscriber
{
    public static class Functions
    {
        public static Subscriber Subscriber;

        static Functions()
        {
            
        }

        [ExcelFunction(Description = "Subscribe to a zmq topic, outputs value to cell")]
        public static void Subscribe(string topic)
        {
            var caller = XlCall.Excel(XlCall.xlfCaller) as ExcelReference;
            Subscriber.Subscribe<string>(topic, (message) =>
            {
                ExcelAsyncUtil.QueueAsMacro(() => { caller.SetValue(message.Data); });
            });
        }
    }
}