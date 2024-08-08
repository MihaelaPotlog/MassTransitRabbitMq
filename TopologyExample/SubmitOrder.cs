namespace TopologyExample
{
    public record SubmitOrder
    {
        public string CustomerType { get; init; }
        public Guid TransactionId { get; init; }
        // ...
    }
}