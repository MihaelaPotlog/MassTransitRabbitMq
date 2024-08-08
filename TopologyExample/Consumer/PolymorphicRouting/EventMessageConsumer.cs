using MassTransit;

namespace Consumer.PolymorphicRouting;

public class EventMessageConsumer : IConsumer<EventMessage>
{
  public Task Consume(ConsumeContext<EventMessage> context)
  {
    throw new NotImplementedException();
  }
}

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
  public Task Consume(ConsumeContext<OrderCreated> context)
  {
    throw new NotImplementedException();
  }
}

public class UserUpdatedConsumer : IConsumer<UserUpdated>
{
  public Task Consume(ConsumeContext<UserUpdated> context)
  {
    throw new NotImplementedException();
  }
}
