using CommandConsumer;
using MassTransit;
using MassTransit.Logging;
using MassTransit.Monitoring;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
  // Topic exchange
  x.UsingRabbitMq((context, cfg) =>
  {
    cfg.ReceiveEndpoint("create-terminal-transaction", x =>
    {
      x.ConfigureConsumeTopology = false;
      x.PrefetchCount = 20;
      x.ConcurrentMessageLimit = 20;
      x.Consumer<CreateTerminalTransactionConsumer>();
    });

    cfg.Host("amqp://guest:guest@localhost:5672", h =>
    {
      h.Username("guest");
      h.Password("guest");
    });
  });
});

void ConfigureResource(ResourceBuilder r)
{
  r.AddService("Service Name",
      serviceVersion: "Version",
      serviceInstanceId: Environment.MachineName);
}

builder.Services.AddOpenTelemetry()
    .ConfigureResource(ConfigureResource)
     .WithMetrics(b => b
        .AddMeter(InstrumentationOptions.MeterName) // MassTransit Meter
        .AddConsoleExporter());// Any OTEL suportable exporter can be used here
    //.WithTracing(b => b
    //    .AddSource(DiagnosticHeaders.DefaultListenerName) // MassTransit ActivitySource
    //    .AddConsoleExporter() // Any OTEL suportable exporter can be used here
    //);


var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
