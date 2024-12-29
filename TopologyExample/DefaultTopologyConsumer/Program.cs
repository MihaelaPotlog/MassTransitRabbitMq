using DefaultTopologyConsumer;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
  x.AddConsumer<TransportResourceGroupChangedConsumer>();
  x.AddConsumer<TestTransportResourceGroupChangedConsumer>();

  //EndpointConvention.Map<TransportResourceGroupChanged>(new Uri("exchange:transport-resource-group-changed"));
  x.SetKebabCaseEndpointNameFormatter();

  // A Transport
  x.UsingRabbitMq((context, cfg) =>
  {
    cfg.MessageTopology.SetEntityNameFormatter(new CustomEntityNameFormatter());


    cfg.ConfigureEndpoints(context);
    cfg.Host("amqp://guest:guest@localhost:5672", h =>
    {
      h.Username("guest");
      h.Password("guest");
    });
  });
});


var app = builder.Build();

app.Run();
