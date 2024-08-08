using MassTransit;

namespace Producer.HeadersExchangesSolution;

[MessageUrn("TopologyExample:PayReceived")]
public class PayReceived
{
    public int OfferId { get; set; }
}
