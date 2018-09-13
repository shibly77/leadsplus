namespace Contact.DomainEventHandlers.ContactCreatedEvent
{
    using Agent.Domain.Events;
    using LeadsPlus.BuildingBlocks.EventBus.Abstractions;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using LeadsPlus.Core;
    using Agent.Services;
    using Agent.Domain;
    using Agent.TypeFormIntegration;

    public class CreateTypeformWhenAgentCreatedDomainEventHandler
                        : INotificationHandler<AgentCreatedDomainEvent>
    {
        private readonly ILoggerFactory _logger;
        private readonly IIdentityService _identityService;
        private readonly IEventBus _eventBus;

        public CreateTypeformWhenAgentCreatedDomainEventHandler(
            ILoggerFactory logger,
            IIdentityService identityService,
            IEventBus eventBus)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(AgentCreatedDomainEvent agentCreatedDomainEvent, CancellationToken cancellationToken)
        {
            var typeFormUrl = await CreateTypeformUrl(agentCreatedDomainEvent.Agent);
            //Do create typeform
           _logger.CreateLogger(nameof(agentCreatedDomainEvent)).LogTrace($"Agent xxx {agentCreatedDomainEvent.Agent.Email}.");
        }

        private async Task<string> CreateTypeformUrl(Agent agent)
        {
            dynamic typeform = await TypeFormCreator.GetTemplateFormAsync();

            typeform.title = $"{agent.Firstname}_{agent.Lastname}_{agent.Email}_{agent.Id}";
            typeform.id = "";

            return await TypeFormCreator.CreateTypeFormAsync(typeform);
        }

        private async Task<string> CreateSpreadsheetForTypeformResponse(Agent agent)
        {

            dynamic typeform = await TypeFormCreator.GetTemplateFormAsync();

            typeform.title = $"{agent.Firstname}_{agent.Lastname}_{agent.Email}_{agent.Id}";
            typeform.id = "";

            return await TypeFormCreator.CreateTypeFormAsync(typeform);
        }
    }
}
