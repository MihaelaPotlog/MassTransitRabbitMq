using MassTransit;

namespace TopologyConsumer
{
    public class PayProcessedConsumer :
        IConsumer<PayProcessed>
    {
        public Task Consume(ConsumeContext<PayProcessed> context)
        {
            throw new NotImplementedException();
        }
    }
}