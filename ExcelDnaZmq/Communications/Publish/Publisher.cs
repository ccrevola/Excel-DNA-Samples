using System.Threading;
using Communications.Messaging;
using Newtonsoft.Json;
using ZeroMQ;

namespace Communications.Publish
{
    public class Publisher
    {
        private CancellationToken _token;
        private ZContext _ctx;
        private ZSocket _publisher;
        private readonly CancellationTokenSource _cts;

        public Publisher()
        {
            _cts = new CancellationTokenSource();
        }

        public void Run()
        {
            _token = _cts.Token;
            _ctx = new ZContext();
            _publisher = new ZSocket(_ctx, ZSocketType.PUB);
            _publisher.Bind("tcp://*:5555");
        }

        public void Publish<T>(Message<T> message) where T: class
        {
            _publisher.Send(message.ToZMessage());
        }

        public void Stop()
        {
            _cts.Cancel();
            _ctx.Shutdown();
        }
    }
}