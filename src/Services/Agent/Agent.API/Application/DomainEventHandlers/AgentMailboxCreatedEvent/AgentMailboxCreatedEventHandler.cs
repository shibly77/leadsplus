﻿namespace Contact.DomainEventHandlers.ContactCreatedEvent
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
    using Agent.Domain.Query;
    using MongoDB.Driver;
    using Agent.EmailCreator;

    public class AgentMailboxCreatedEventHandler
                        : INotificationHandler<AgentCreatedDomainEvent>, INotificationHandler<AgentMailboxUpdatedEvent>
    {
        private readonly ILoggerFactory logger;
        private readonly IIdentityService identityService;
        private readonly IEventBus eventBus;

        public AgentMailboxCreatedEventHandler(
            ILoggerFactory logger,
            IIdentityService identityService,
            IEventBus eventBus)
        {
            this.identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(AgentCreatedDomainEvent agentCreatedDomainEvent, CancellationToken cancellationToken)
        {
            await Processor(agentCreatedDomainEvent.Agent);

            logger.CreateLogger(nameof(agentCreatedDomainEvent)).LogTrace($"Agent mailbox created for {agentCreatedDomainEvent.Agent.Id}.");
        }

        public async Task Handle(AgentMailboxUpdatedEvent agentMailboxUpdatedEvent, CancellationToken cancellationToken)
        {
            await Processor(agentMailboxUpdatedEvent.Agent);

            logger.CreateLogger(nameof(agentMailboxUpdatedEvent)).LogTrace($"Agent mailbox created for {agentMailboxUpdatedEvent.Agent.Id}.");
        }

        private async Task Processor(Agent agent)
        {
            string mailboxName = $"{agent.Email.Split("@")[0]}";

            var integrationEmail = await CreateIntigrationEmail(mailboxName);

            var filter = Builders<Agent>.Filter.Eq("Id", agent.Id);
            var update = Builders<Agent>.Update
                .Set("IntegrationEmail", integrationEmail)
                .CurrentDate("UpdatedDate");
        }

        public async Task<string> CreateIntigrationEmail(string mailboxName)
        {
            var emailAccount = new EmailAccount
            {
                Domain = "adfenixleads.com",//should come from config
                UserName = mailboxName,
                Password = "chngeme",
                Quota = 400
            };

            return await new MailboxCreator(emailAccount).Create();
        }
    }
}
