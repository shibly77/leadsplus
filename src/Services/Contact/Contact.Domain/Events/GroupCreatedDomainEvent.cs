namespace Contact.Domain.Events
{
    public class GroupCreatedDomainEvent : Event
    {
        public string Name { get; set; }
        public string OwnerId { get; set; }
    }
}
