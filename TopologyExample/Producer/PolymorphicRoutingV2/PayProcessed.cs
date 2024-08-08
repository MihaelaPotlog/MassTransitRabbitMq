using MassTransit;

namespace Producer.PolymorphicRoutingV2;

[MessageUrn("TopologyExample:PayProcessed")]
public record PayProcessed
{
    public string OfferId { get; init; }
    public int ContractType { get; init; }
    // ...
}

[MessageUrn("TopologyExample:WebsitePayProcessed")]
public record WebsitePayProcessed : PayProcessed
{
}

[MessageUrn("TopologyExample:DriverPayProcessed")]
public record DriverPayProcessed : PayProcessed
{
}
