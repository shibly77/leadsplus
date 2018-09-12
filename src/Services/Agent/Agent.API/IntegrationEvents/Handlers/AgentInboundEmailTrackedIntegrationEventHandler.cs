namespace Agent.IntegrationEvents
{
    using Agent.Repositories;
    using Agent.Services;
    using LeadsPlus.BuildingBlocks.EventBus.Abstractions;
    using System;
    using System.Threading.Tasks;

    public class AgentInboundEmailTrackedIntegrationEventHandler
        : IIntegrationEventHandler<AgentInboundEmailTrackedIntegrationEvent>
    {
        private readonly IEventBus eventBus;
        private readonly IAgentRepository agentRepository;

        public AgentInboundEmailTrackedIntegrationEventHandler(IEventBus eventBus,
            IAgentRepository agentRepository)
        {
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.agentRepository = agentRepository ?? throw new ArgumentNullException(nameof(agentRepository));
        }

        public async Task Handle(AgentInboundEmailTrackedIntegrationEvent @event)
        {
            var agent = await agentRepository.GetByIntegrationEmailAsync(@event.AgentEmail);

            var subject = $"Thanks! you for your enquiry";

            var mailBody = $"Hi,\n.Thanks!\nWhy stop here? Thanks for helping us build trust and empowering agent.)\nTell up something more about yourself {agent.AgentTypeForm.TypeFormUrl}";

            var emailNeedsToBeSent = new EmailNeedsToBeSentIntegrationEvent
            {
                Body = mailBody,
                IsBodyHtml = false,
                Subject = subject,
                To = new[] { @event.CustomerEmail },
                ReplyTo = string.Empty,
                AggregateId = @event.AggregateId
            };

            eventBus.Publish(emailNeedsToBeSent);

            var createContactIntegrationEvent = new CreateContactIntegrationEvent()
            {
                AggregateId = @event.AggregateId,
                Source = "InnoundEmail",
                Email = @event.CustomerEmail
            };

            eventBus.Publish(createContactIntegrationEvent);
        }
    }
}
