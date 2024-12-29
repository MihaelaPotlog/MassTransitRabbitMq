using MassTransit;

namespace CommandConsumer;

public class CreateTerminalTransactionConsumer : IConsumer<CreateTerminalTransactionCommand>
{
  public async Task Consume(ConsumeContext<CreateTerminalTransactionCommand> context)
  {
    Console.WriteLine("Processing" + context.Message.MerchantReference);
    await Task.Delay(10000);
  }
}
