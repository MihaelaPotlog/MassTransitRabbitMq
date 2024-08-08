using Consumer.PolymorphicRouting;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMassTransit(x =>
{
  // Topic exchange
  x.UsingRabbitMq((context, cfg) =>
  {
    cfg.ReceiveEndpoint("order_queue", e =>
      {
        e.Consumer<OrderCreatedConsumer>();
      });

    cfg.ReceiveEndpoint("user_queue", e =>
      {
        e.Consumer<UserUpdatedConsumer>();
      });

    cfg.ReceiveEndpoint("event_queue", e =>
      {
        e.Consumer<EventMessageConsumer>();
      });

    cfg.Host("amqp://guest:guest@localhost:5672", h =>
      {
        h.Username("guest");
        h.Password("guest");
      });
  });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
