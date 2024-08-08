using MassTransit;
using TopologyExample;

namespace TopologyConsumer
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