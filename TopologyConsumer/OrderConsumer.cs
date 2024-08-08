using MassTransit;
using TopologyExample;

public class OrderConsumer :
    IConsumer<SubmitOrder>
{
    public async Task Consume(ConsumeContext<SubmitOrder> context)
    {
        throw new NotImplementedException();
    }
}