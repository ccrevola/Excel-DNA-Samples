using Newtonsoft.Json;
using ZeroMQ;

namespace Communications.Messaging
{
    public static class MessageExtensions
    {
        public static ZMessage ToZMessage<T>(this Message<T> message) where T: class
        {
            var zMessage = new ZMessage();
            var header = new ZFrame(message.Header);
            zMessage.Add(header);
            zMessage.Add(new ZFrame(JsonConvert.SerializeObject(message.Data)));
            return zMessage;
        }
    }
}