using Communications.Messaging;
using Communications.Publish;
using ExcelDna.Integration;

namespace ExcelDnaZmqServer
{
    public static class PublisherFunctions
    {
        public static Publisher Publisher;

        [ExcelFunction(Description = "Publish to a topic")]
        public static void Publish(string topic, string message)
        {
            var msg = new Message<string> {Header = topic};
            for (int i = 0; i < 1000; i++)
            {
                msg.Data = $"{message} - {i}";
                Publisher.Publish(msg);
            }
        }
    }
}