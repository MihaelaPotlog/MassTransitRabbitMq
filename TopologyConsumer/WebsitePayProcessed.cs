namespace TopologyExample
{
    public record WebsitePayProcessed
    {
        public string OfferId { get; init; }
        public int ContractType { get; init; }
        // ...
    }
}