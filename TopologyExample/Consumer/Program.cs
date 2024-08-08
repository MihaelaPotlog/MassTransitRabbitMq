using Consumer.TopicExchangesSolution;
using MassTransit;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
  // Topic exchange
  x.UsingRabbitMq((context, cfg) =>
  {
    #region Topic exchanges
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
    #endregion

    //#region Polymorphic v2
    //cfg.ReceiveEndpoint("web-pay-processed", x =>
    //{
    //  x.ConfigureConsumeTopology = false;

    //  x.Consumer<WebsitePayProcessedConsumer>();

    //  x.Bind("Contracts.WebsitePayProcessed");
    //});


    //cfg.ReceiveEndpoint("driver-pay-processed", x =>
    //{
    //  x.ConfigureConsumeTopology = false;

    //  x.Consumer<DriverPayProcessedConsumer>();

    //  x.Bind("Contracts.DriverPayProcessed");
    //});

    //cfg.ReceiveEndpoint("pay-processed", x =>
    //{
    //  x.ConfigureConsumeTopology = false;

    //  x.Consumer<PayProcessedConsumer>();

    //  x.Bind("Contracts.DriverPayProcessed");
    //  x.Bind("Contracts.WebsitePayProcessed");
    //});
    //#endregion

    //#region Polymorphic routing
    //cfg.ReceiveEndpoint("order_queue", e =>
    //  {
    //    e.Consumer<OrderCreatedConsumer>();
    //  });

    //cfg.ReceiveEndpoint("user_queue", e =>
    //  {
    //    e.Consumer<UserUpdatedConsumer>();
    //  });

    //cfg.ReceiveEndpoint("event_queue", e =>
    //  {
    //    e.Consumer<EventMessageConsumer>();
    //  });
    //#endregion

    //#region Header exchanges
    //cfg.ReceiveEndpoint("kiosk-pay-received-queue", e =>
    //  {
    //    e.Consumer<KioskPayReceivedConsumer>();
    //    e.Bind("pay-received", s =>
    //          {
    //            s.ExchangeType = ExchangeType.Headers;
    //            s.SetBindingArgument("Origin", "kiosk");
    //            s.SetBindingArgument("x-match", "all");
    //          }
    //      );
    //  });
    //#endregion

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
