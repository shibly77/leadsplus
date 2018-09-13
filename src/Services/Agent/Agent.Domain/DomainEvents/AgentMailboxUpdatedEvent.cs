namespace Agent.Domain.Events
{
    using MediatR;
    using System;

    public class AgentMailboxUpdatedEvent : INotification
    {
        public Agent Agent { get; set; }
        public string NewMailbox { get; set; }
    }
}
