using MassTransit;

namespace Consumer.PolymorphicRoutingV2;

[MessageUrn("TopologyExample:PayProcessed")]
public class PayProcessed
{
  public string OfferId { get; init; }
  public int ContractType { get; init; }
}
