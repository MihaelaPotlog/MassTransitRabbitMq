using Consumer.PolymorphicRoutingV2;
using MassTransit;

namespace PolymorphicRoutingV2Consumer.Consumers
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
