using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Communications.Messaging;
using Newtonsoft.Json;
using ZeroMQ;

namespace Communications.Subscribe
{
    public class Subscriber
    {
        private CancellationToken _token;
        private ZContext _ctx;
        private ZSocket _subscriber;
        private readonly CancellationTokenSource _cts;
        private string _connection;

        public Subscriber(string connection)
        {
            _cts = new CancellationTokenSource();
            _token = _cts.Token;
            _ctx = new ZContext();
            _connection = connection;
        }

        public Task Subscribe<T>(string topic, Action<Message<T>> callback)  where T: class
        {
            return Task.Run(() =>
            {
                _subscriber = new ZSocket(_ctx, ZSocketType.SUB);
                _subscriber.Connect(_connection);
                _subscriber.SetOption(ZSocketOption.SUBSCRIBE, topic);
                var receiver = ZPollItem.CreateReceiver();
                while (!_token.IsCancellationRequested)
                {
                    if (_subscriber.PollIn(receiver, out var message, out var error, TimeSpan.FromMilliseconds(64)))
                    {
                        if (message.Count != 2) throw new Exception("Wrong message format");
                        var header = message[0].ReadString();
                        var d = message[1].ReadString();
                        var data = JsonConvert.DeserializeObject<T>(d);
                        callback.Invoke(new Message<T>
                        {
                            Header = header,
                            Data = data
                        });
                    }
                }
            }, _token);
        }

        public void Stop()
        {
            _cts.Cancel();
            _ctx.Shutdown();
        }
    }
}