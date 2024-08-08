using Consumer.PolymorphicRoutingV2;
using MassTransit;

namespace PolymorphicRoutingV2Consumer.Consumers
{
    public class DriverPayProcessedConsumer :
        IConsumer<DriverPayProcessed>
    {

        public Task Consume(ConsumeContext<DriverPayProcessed> context)
        {
            throw new NotImplementedException();
        }
    }
}
