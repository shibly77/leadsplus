namespace Contact.Domain.Events
{
    public class ContactCopiedToGroupDomainEvent : Event
    {
        public string[] ContactIds { get; set; }
    }
}
