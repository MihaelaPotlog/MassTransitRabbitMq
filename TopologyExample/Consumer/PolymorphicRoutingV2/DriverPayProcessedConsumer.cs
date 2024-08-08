using MassTransit;

namespace Consumer.PolymorphicRoutingV2
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
