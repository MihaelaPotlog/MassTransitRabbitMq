using MassTransit;
using TopologyExample;

public class WebsitePayProcessedConsumer :
    IConsumer<WebsitePayProcessed>
{
    public Task Consume(ConsumeContext<WebsitePayProcessed> context)
    {
        throw new NotImplementedException();
    }
}