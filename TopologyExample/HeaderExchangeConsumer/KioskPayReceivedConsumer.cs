using MassTransit;

namespace Consumer.HeadersExchangesSolution;

public class KioskPayReceivedConsumer : IConsumer<PayReceived>
{
  public Task Consume(ConsumeContext<PayReceived> context)
  {
    throw new NotImplementedException();
  }
}
