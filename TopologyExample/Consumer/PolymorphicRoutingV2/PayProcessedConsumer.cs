using MassTransit;

namespace Consumer.PolymorphicRoutingV2
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
