using MassTransit;

namespace Producer.TopicExchangesSolution;

[MessageUrn("TopologyExample:PaymentProcessed")]
public record PaymentProcessed
{
  public string Origin { get; init; }
  public Guid TransactionId { get; init; }
  // ...
}
