using MassTransit;

namespace DefaultTopologyConsumer
{
  public class TestTransportResourceGroupChangedConsumer : IConsumer<TransportResourceGroupChanged>
  {
    public Task Consume(ConsumeContext<TransportResourceGroupChanged> context)
    {
      throw new NotImplementedException();
    }
  }
}
