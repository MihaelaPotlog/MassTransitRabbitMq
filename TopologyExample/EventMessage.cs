namespace TopologyEventMessage
{
    public class EventMessage
    {
        public string Message { get; set; }
    }

    public class OrderCreated : EventMessage
    {
        public int OrderId { get; set; }
    }

    public class UserUpdated : EventMessage
    {
        public int UserId { get; set; }
    }
}
