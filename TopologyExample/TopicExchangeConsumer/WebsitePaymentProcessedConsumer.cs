using MassTransit;

namespace Consumer.TopicExchangesSolution;

public class WebsitePaymentProcessedConsumer :
    IConsumer<PaymentProcessed>
{
  public async Task Consume(ConsumeContext<PaymentProcessed> context)
  {
    throw new NotImplementedException();
  }
}
