using MassTransit;
using Consumer.PolymorphicRoutingV2;

public class WebsitePayProcessedConsumer :
    IConsumer<WebsitePayProcessed>
{
    public Task Consume(ConsumeContext<WebsitePayProcessed> context)
    {
        throw new NotImplementedException();
    }
}