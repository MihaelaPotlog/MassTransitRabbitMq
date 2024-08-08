using MassTransit;

namespace Consumer.PolymorphicRoutingV2;

[MessageUrn("TopologyExample:DriverPayProcessed")]
public class DriverPayProcessed
  {
      public string OfferId { get; init; }
      public int ContractType { get; init; }
  }
