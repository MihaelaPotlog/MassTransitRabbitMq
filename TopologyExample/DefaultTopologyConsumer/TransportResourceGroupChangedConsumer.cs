using MassTransit;

namespace DefaultTopologyConsumer;

public class TransportResourceGroupChangedConsumer : IConsumer<TransportResourceGroupChanged>
{
  public Task Consume(ConsumeContext<TransportResourceGroupChanged> context)
  {
    throw new NotImplementedException();
  }
}
