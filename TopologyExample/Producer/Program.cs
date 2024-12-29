using CommandConsumer;
using MassTransit;
using Producer;
using Producer.HeadersExchangesSolution;
using Producer.PolymorphicRouting;
using Producer.PolymorphicRoutingV2;
using Producer.TopicExchangesSolution;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSendObserver<SendObserverTest>();
builder.Services.AddMassTransit(x =>
{
  // A Transport
  x.UsingRabbitMq((context, cfg) =>
  {
    #region Topic exchange
    cfg.Send<PaymentProcessed>(x =>
      {

        // use customerType for the routing key
        x.UseRoutingKeyFormatter(context => context.Message.Origin);

        // multiple conventions can be set, in this case also CorrelationId
        //x.UseCorrelationId(context => context.Message.TransactionId);
      });

    // Keeping in mind that the default exchange config for your published type will be the full typename of your message
    // we explicitly specify which exchange the message will be published to. So it lines up with the exchange we are binding our
    // consumers too.
    cfg.Message<PaymentProcessed>(x => x.SetEntityName("Contracts.PaymentProcessed"));

    // Also if your publishing your message: because publishing a message will, by default, send it to a fanout queue.
    // We specify that we are sending it to a direct queue instead. In order for the routingkeys to take effect.
    cfg.Publish<PaymentProcessed>(x => x.ExchangeType = ExchangeType.Topic);
    #endregion


    #region Polymorphism routing
    cfg.Message<EventMessage>(x => x.SetEntityName("Contracts.EventMessage"));
    cfg.Message<OrderCreated>(x => x.SetEntityName("Contracts.OrderCreated"));
    cfg.Message<UserUpdated>(x => x.SetEntityName("Contracts.UserUpdated"));
    #endregion


    #region Polymorphism routing v2
    // Varianta cu base class si child classes

    cfg.Message<WebsitePayProcessed>(x => x.SetEntityName("Contracts.WebsitePayProcessed"));
    cfg.Publish<WebsitePayProcessed>();

    cfg.Message<DriverPayProcessed>(x => x.SetEntityName("Contracts.DriverPayProcessed"));
    cfg.Publish<DriverPayProcessed>();
    //cfg.Publish<EventMessage>();
    #endregion

    #region Header exchange
    cfg.Message<PayReceived>(x => x.SetEntityName("pay-received"));
    #endregion

    cfg.Host("amqp://guest:guest@localhost:5672", h =>
      {
        h.Username("guest");
        h.Password("guest");
      });
  });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();



app.MapPost("/topic-exchanges", async (IPublishEndpoint provider) =>
{
  await provider.Publish(new PaymentProcessed { Origin = "WEBSITE", TransactionId = Guid.NewGuid() });
})
.WithName("PublishTopicExchange");

app.MapPost("/polymorphism-routing", async (IPublishEndpoint provider) =>
{
  await provider.Publish(new OrderCreated { OrderId = 123, Message = "Order Created" });
  await provider.Publish(new UserUpdated { UserId = 456, Message = "User Updated" });
})
.WithName("UsePolymorphismRouting");

app.MapPost("/polymorphism-routing-v2", async (IPublishEndpoint provider) =>
{
  await provider.Publish(new WebsitePayProcessed
  {
    ContractType = 1,
    OfferId = "88"
  });
})
.WithName("UseExtraExchange");

app.MapPost("/headers-exchange", async (IPublishEndpoint provider) =>
{
  await provider.Publish(new PayReceived
  {
    OfferId = 88
  });
})
.WithName("PublishOnHeadersExchange");


app.MapPost("/create-terminal-transaction", async (ISendEndpointProvider sendProvider) =>
{
  var endpoint = await sendProvider.GetSendEndpoint(new Uri("queue:create-terminal-transaction"));

  for (int i = 0; i < 100; i++)
  {
    await endpoint.Send(new CreateTerminalTransactionCommand
    {
      MerchantReference = i.ToString()
    });
  }
});


app.Run();
