using DefaultTopologyProducer;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
  // A Transport
  x.UsingRabbitMq((context, cfg) =>
  {
    cfg.MessageTopology.SetEntityNameFormatter(new CustomEntityNameFormatter());

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



app.MapPost("/default-exchanges", async (IPublishEndpoint provider) =>
{
  await provider.Publish(new TransportResourceGroupChanged { TransportId = 12 });
})
.WithName("PublishOnDefaultExchange");

app.Run();
