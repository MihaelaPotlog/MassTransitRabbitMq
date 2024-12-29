using MassTransit;

namespace Producer
{
  public class SendObserverTest : ISendObserver
  {
    public Task PostSend<T>(SendContext<T> context) where T : class
    {
      Console.WriteLine(context.Message.ToString());
      return Task.CompletedTask;
    }

    public Task PreSend<T>(SendContext<T> context) where T : class
    {
      return Task.CompletedTask;
    }

    public Task SendFault<T>(SendContext<T> context, Exception exception) where T : class
    {
      return Task.CompletedTask;
    }
  }
}
