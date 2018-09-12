namespace Agent.DomainEventHandlers.ContactCreatedEvent
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
    using Agent.Domain.Query;
    using LeadsPlus.Core.Query;
    using Agent.EmailCreator;

    public class AgentMailboxCreatedEvent
                        : INotificationHandler<AgentCreatedDomainEvent>
    {
        private readonly ILoggerFactory logger;
        private readonly IRepository<Agent> agnetRepository;
        private readonly IIdentityService identityService;
        private readonly IEventBus eventBus;
        private readonly IQueryExecutor queryExecutor;

        public AgentMailboxCreatedEvent(
            ILoggerFactory logger,
            IRepository<Agent> agentRepository,
            IIdentityService identityService,
            IEventBus eventBus,
            IQueryExecutor queryExecutor)
        {
            this.identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        public async Task Handle(AgentCreatedDomainEvent agentCreatedDomainEvent, CancellationToken cancellationToken)
        {
            var agent = await queryExecutor.Execute<GetAgentQuery, Agent>(new GetAgentQuery() { AgentId = agentCreatedDomainEvent.Agent.Id });

            

            //Do create typeform
            logger.CreateLogger(nameof(agentCreatedDomainEvent)).LogTrace($"Agent xxx {agentCreatedDomainEvent.Agent.Email}.");
        }

        
    }
}
