namespace Contact.Domain.Events
{
    public class ContactRemovedFromGroupDomainEvent : Event
    {
        public string[] ContactIds { get; set; }
    }
}
