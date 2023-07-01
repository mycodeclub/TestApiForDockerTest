using System.Threading.Channels;

namespace TestApiForDockerTest
{
    public class OrderProcessor : IHostedService
    {
        private readonly Channel<string> _channel;

        public OrderProcessor(Channel<string> channel)
        {
            _channel = channel;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                while (!_channel.Reader.Completion.IsCompleted)
                {
                    var response = await _channel.Reader.ReadAsync();
                    // processing message ...
                    await Task.Delay(1000);
                    Console.WriteLine("From OrderProcessor StartAsyncMethod" + response);
                }
            });
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
