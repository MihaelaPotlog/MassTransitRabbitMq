using MassTransit;

namespace DefaultTopologyConsumer;

[MessageUrn("DefaultTopology:TransportResourceGroupChanged")]
public class TransportResourceGroupChanged
{
  public int TransportId { get; set; }
}
