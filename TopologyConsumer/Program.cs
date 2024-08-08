using MassTransit;
using RabbitMQ.Client;
using TopologyConsumer;

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
        cfg.ReceiveEndpoint("priority-orders", x =>
        {
            x.ConfigureConsumeTopology = false;

            x.Consumer<OrderConsumer>();

            x.Bind("submitorder", s =>
            {
                s.RoutingKey = "PRIORITY";
                s.ExchangeType = ExchangeType.Topic;
            });
        });

        cfg.ReceiveEndpoint("regular-orders", x =>
        {
            x.ConfigureConsumeTopology = false;

            x.Consumer<OrderConsumer>();

            x.Bind("submitorder", s =>
            {
                s.RoutingKey = "REGULAR";
                s.ExchangeType = ExchangeType.Topic;
            });
        });

        cfg.ReceiveEndpoint("all-orders", x =>
        {
            x.ConfigureConsumeTopology = false;

            x.Consumer<OrderConsumer>();

            x.Bind("submitorder", s =>
            {
                s.RoutingKey = "*";
                s.ExchangeType = ExchangeType.Topic;
            });
        });

        // 
        //cfg.ReceiveEndpoint("web-pay-processed", x =>
        //{
        //    x.ConfigureConsumeTopology = false;

        //    x.Consumer<WebsitePayProcessedConsumer>();

        //    x.Bind("Contracts.WebsitePayProcessed");
        //});


        //cfg.ReceiveEndpoint("driver-pay-processed", x =>
        //{
        //    x.ConfigureConsumeTopology = false;

        //    x.Consumer<DriverPayProcessedConsumer>();

        //    x.Bind("Contracts.DriverPayProcessed");
        //});

        //cfg.ReceiveEndpoint("pay-processed", x =>
        //{
        //    x.ConfigureConsumeTopology = false;

        //    x.Consumer<PayProcessedConsumer>();

        //    x.Bind("Contracts.DriverPayProcessed");
        //    x.Bind("Contracts.WebsitePayProcessed");
        //});

        // Polymorphic routing
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}