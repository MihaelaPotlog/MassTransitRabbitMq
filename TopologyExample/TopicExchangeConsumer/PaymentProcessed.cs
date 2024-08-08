using MassTransit;

namespace Consumer.TopicExchangesSolution;

[MessageUrn("TopologyExample:PaymentProcessed")]
public record PaymentProcessed
{
  public string CustomerType { get; init; }
  public Guid TransactionId { get; init; }
  // ...
}
