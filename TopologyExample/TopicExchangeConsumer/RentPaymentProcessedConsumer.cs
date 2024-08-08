using MassTransit;

namespace Consumer.TopicExchangesSolution;

public class RentPaymentProcessedConsumer :
    IConsumer<PaymentProcessed>
{
  public async Task Consume(ConsumeContext<PaymentProcessed> context)
  {
    throw new NotImplementedException();
  }
}