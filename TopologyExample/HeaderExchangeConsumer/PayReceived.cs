using MassTransit;

namespace Consumer.HeadersExchangesSolution;

[MessageUrn("TopologyExample:PayReceived")]
public class PayReceived
{
  public int OfferId { get; set; }
}
