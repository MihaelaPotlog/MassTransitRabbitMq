using MassTransit;

namespace Consumer.PolymorphicRoutingV2;

[MessageUrn("TopologyExample:WebsitePayProcessed")]
public record WebsitePayProcessed
  {
      public string OfferId { get; init; }
      public int ContractType { get; init; }
      // ...
  }
