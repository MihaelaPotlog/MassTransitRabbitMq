using MassTransit;
using PolymorphicRoutingV2Consumer.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
  x.UsingRabbitMq((context, cfg) =>
  {
    cfg.ReceiveEndpoint("web-pay-processed", x =>
    {
      x.ConfigureConsumeTopology = false;

      x.Consumer<WebsitePayProcessedConsumer>();

      x.Bind("Contracts.WebsitePayProcessedffgds");
    });


    cfg.ReceiveEndpoint("driver-pay-processed", x =>
    {
      x.ConfigureConsumeTopology = false;

      x.Consumer<DriverPayProcessedConsumer>();

      x.Bind("Contracts.DriverPayProcessed");
    });

    cfg.ReceiveEndpoint("pay-processed", x =>
    {
      x.ConfigureConsumeTopology = false;

      x.Consumer<PayProcessedConsumer>();

      x.Bind("Contracts.DriverPayProcessed");
      x.Bind("Contracts.WebsitePayProcessed");
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
