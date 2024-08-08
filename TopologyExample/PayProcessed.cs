namespace TopologyExample
{
    public record PayProcessed
    {
        public string OfferId { get; init; }
        public int ContractType { get; init; }
        // ...
    }

    public record WebsitePayProcessed : PayProcessed
    {
    }

    public record DriverPayProcessed : PayProcessed
    {
    }
}