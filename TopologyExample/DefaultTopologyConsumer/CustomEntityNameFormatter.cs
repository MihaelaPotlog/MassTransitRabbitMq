using MassTransit;

namespace DefaultTopologyConsumer;

/// <summary>
/// Creates a custom entity name format to be used when creating the message-bound exchanges
/// that we publish (broadcast) to. We use the custom format to override the default one.
///
/// By default Mass Transit uses a namespace dependent format: "Namespace:Message".
/// </summary>
internal sealed class CustomEntityNameFormatter : IEntityNameFormatter
{
    public string FormatEntityName<T>()
    {
        return $"Contracts.{typeof(T).Name}";
    }
}
