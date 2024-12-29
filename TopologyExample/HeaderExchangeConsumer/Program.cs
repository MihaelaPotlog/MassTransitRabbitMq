using Consumer.HeadersExchangesSolution;
using MassTransit;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
  // Topic exchange
  x.UsingRabbitMq((context, cfg) =>
  {
    cfg.ReceiveEndpoint("kiosk-pay-received-queue", e =>
      {
        e.Consumer<KioskPayReceivedConsumer>();
        e.Bind("pay-received", s =>
              {
                s.ExchangeType = ExchangeType.Headers;
                s.SetBindingArgument("Origin", "kiosk");
                s.SetBindingArgument("x-match", "all");
              }
          );
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
