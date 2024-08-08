using MassTransit;

namespace Consumer.PolymorphicRouting;

[MessageUrn("TopologyExample:EventMessage")]
public class EventMessage
{
  public string Message { get; set; }
}

[MessageUrn("TopologyExample:OrderCreated")]
public class OrderCreated : EventMessage
{
  public int OrderId { get; set; }
}

[MessageUrn("TopologyExample:UserUpdated")]
public class UserUpdated : EventMessage
{
  public int UserId { get; set; }
}
