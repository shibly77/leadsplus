namespace Agent.Domain.Events
{
    using MediatR;
    using System;

    public class AgentTypeformUpdatedEvent : INotification
    {
        public Agent Agent { get; set; }
    }
}
