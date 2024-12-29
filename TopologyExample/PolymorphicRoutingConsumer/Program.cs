using Consumer.PolymorphicRouting;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
  // Topic exchange
  x.UsingRabbitMq((context, cfg) =>
  {
    //cfg.Message<EventMessage>(x => x.SetEntityName("Contracts.EventMessage"));
    //cfg.Message<OrderCreated>(x => x.SetEntityName("Contracts.OrderCreated"));
    //cfg.Message<UserUpdated>(x => x.SetEntityName("Contracts.UserUpdated"));

    cfg.ReceiveEndpoint("order_queue", e =>
      {
        e.ConfigureConsumeTopology = false;
        e.Consumer<OrderCreatedConsumer>();
        e.Bind("Contracts.OrderCreated");
      });

    cfg.ReceiveEndpoint("user_queue", e =>
      {
        e.ConfigureConsumeTopology = false;
        e.Consumer<UserUpdatedConsumer>();
        e.Bind("Contracts.UserUpdated");
      });

    cfg.ReceiveEndpoint("event_queue", e =>
      {
        e.ConfigureConsumeTopology = false;
        e.Consumer<EventMessageConsumer>();
        e.Bind("Contracts.EventMessage");
      });

    cfg.Host("amqp://guest:guest@localhost:5672", h =>
      {
        h.Username("guest");
        h.Password("guest");
      });
  });
});


var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
