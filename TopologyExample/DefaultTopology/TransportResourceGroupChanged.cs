using MassTransit;

namespace DefaultTopologyProducer;

[MessageUrn("DefaultTopology:TransportResourceGroupChanged")]
public class TransportResourceGroupChanged
{
  public int TransportId { get; set; }
}
