using MassTransit;
using TopologyExample;

namespace TopologyConsumer
{
    public class KioskPayReceivedConsumer : IConsumer<PayReceived>
    {
        public Task Consume(ConsumeContext<PayReceived> context)
        {
            throw new NotImplementedException();
        }
    }
}
