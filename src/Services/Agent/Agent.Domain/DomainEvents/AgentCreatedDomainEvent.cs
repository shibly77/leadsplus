namespace Agent.Domain.Events
{
    using MediatR;
    using System;

    public class AgentCreatedDomainEvent : INotification
    {
        public Agent Agent { get; set; }
    }
}
