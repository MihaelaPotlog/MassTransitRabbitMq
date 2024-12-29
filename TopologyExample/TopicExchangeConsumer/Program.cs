using Consumer.TopicExchangesSolution;
using MassTransit;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
  // Topic exchange
  x.UsingRabbitMq((context, cfg) =>
  {
    cfg.ReceiveEndpoint("rent-payment-processed", x =>
      {
        x.ConfigureConsumeTopology = false;

        x.Consumer<RentPaymentProcessedConsumer>();

        x.Bind("Contracts.PaymentProcessed", s =>
          {
            s.RoutingKey = "RENT";
            s.ExchangeType = ExchangeType.Topic;
          });
      });

    cfg.ReceiveEndpoint("website-payment-processed", x =>
      {
        x.ConfigureConsumeTopology = false;

        x.Consumer<RentPaymentProcessedConsumer>();

        x.Bind("Contracts.PaymentProcessed", s =>
          {
            s.RoutingKey = "WEBSITE";
            s.ExchangeType = ExchangeType.Topic;
          });
      });

    cfg.ReceiveEndpoint("all-payment-processed", x =>
      {
        x.ConfigureConsumeTopology = false;

        x.Consumer<RentPaymentProcessedConsumer>();

        x.Bind("Contracts.PaymentProcessed", s =>
          {
            s.RoutingKey = "*";
            s.ExchangeType = ExchangeType.Topic;
          });
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
