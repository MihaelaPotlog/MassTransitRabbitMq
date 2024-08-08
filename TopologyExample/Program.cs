using MassTransit;
using RabbitMQ.Client;
using TopologyExample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{
    // A Transport
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Send<SubmitOrder>(x =>
        {

            // use customerType for the routing key
            x.UseRoutingKeyFormatter(context => context.Message.CustomerType);

            // multiple conventions can be set, in this case also CorrelationId
            //x.UseCorrelationId(context => context.Message.TransactionId);
        });

        // Keeping in mind that the default exchange config for your published type will be the full typename of your message
        // we explicitly specify which exchange the message will be published to. So it lines up with the exchange we are binding our
        // consumers too.
        cfg.Message<SubmitOrder>(x => x.SetEntityName("submitorder"));

        //// Also if your publishing your message: because publishing a message will, by default, send it to a fanout queue.
        //// We specify that we are sending it to a direct queue instead. In order for the routingkeys to take effect.
        cfg.Publish<SubmitOrder>(x => x.ExchangeType = ExchangeType.Topic);


        // Varianta cu base class si child classes
        //cfg.Message<WebsitePayProcessed>(x => x.SetEntityName("Contracts.WebsitePayProcessed"));
        //cfg.Publish<WebsitePayProcessed>();

        //cfg.Message<DriverPayProcessed>(x => x.SetEntityName("Contracts.DriverPayProcessed"));
        //cfg.Publish<DriverPayProcessed>();


        //cfg.Message<PayReceived>(x => x.SetEntityName("pay-received"));

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



app.MapPost("/topic-exchange", async (IPublishEndpoint provider) =>
{
    await provider.Publish(new SubmitOrder { CustomerType = "REGULAR", TransactionId = Guid.NewGuid() });
})
.WithName("PublishOnTopicExchange");

//app.MapPost("/", async (IPublishEndpoint provider) =>
//{
//    await provider.Publish(new OrderCreated { OrderId = 123, Message = "Order Created" });
//    await provider.Publish(new UserUpdated { UserId = 456, Message = "User Updated" });
//})
//.WithName("UsePolymorphismRouting");

app.MapPost("/polymorphism-routing", async (IPublishEndpoint provider) =>
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
.WithName("UseHeadersExchange");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}