namespace Agent.Commands
{
    using Agent.Command;
    using Agent.Domain;
    using Agent.Domain.Query;
    using Agent.EmailCreator;
    using Agent.Services;
    using Agent.TypeFormIntegration;
    using LeadsPlus.BuildingBlocks.EventBus.Abstractions;
    using LeadsPlus.Core;
    using LeadsPlus.Core.Query;
    using MediatR;
    using MongoDB.Driver;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    
    public class AgentCommandHandler
        : IRequestHandler<CreateAgentCommand, bool>, 
        IRequestHandler<UpdateAgentCommand, bool>,
        IRequestHandler<DeleteAgentCommand, bool>,
        IRequestHandler<CreateAgentIntigrationEmailAccountCommand, bool>,
        IRequestHandler<CreateAgentTypeFormAccountCommand, bool>
    {
        private readonly IEventBus eventBus;
        private readonly IMediator mediator;
        private readonly IIdentityService identityService;
        private readonly IQueryExecutor queryExecutor;
        private readonly IRepository<Agent> agentRepository;

        public AgentCommandHandler(IMediator mediator, 
            IRepository<Agent> agentRepository, 
            IEventBus eventBus, 
            IIdentityService identityService,
            IQueryExecutor queryExecutor)
        {
            this.agentRepository = agentRepository ?? throw new ArgumentNullException(nameof(agentRepository));
            this.queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
            this.eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

            this.identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(CreateAgentCommand createAgentCommand, CancellationToken cancellationToken)
        {
            createAgentCommand.OwnerId = identityService.GetUserIdentity();

            var agent = new Agent(createAgentCommand.AggregateId,
                createAgentCommand.OwnerId,
                createAgentCommand.Firstname,
                createAgentCommand.Lastname,
                createAgentCommand.City,
                createAgentCommand.Email,
                createAgentCommand.Phone,
                createAgentCommand.Country,
                createAgentCommand.Address,
                createAgentCommand.Company);

            await agentRepository.AddAsync(agent);
            await mediator.DispatchDomainEventsAsync(agent);

            return true;
        }

        public async Task<bool> Handle(UpdateAgentCommand updateAgentCommand, CancellationToken cancellationToken)
        {
            var agent = await queryExecutor.Execute<GetAgentQuery, Agent>(new GetAgentQuery() { AgentId = updateAgentCommand.AggregateId });

            agent = DataMapper.Map<Agent, UpdateAgentCommand>(updateAgentCommand);

            //agent.Firstname = updateAgentCommand.Firstname;
            //agent.Lastname = updateAgentCommand.Lastname;
            //agent.City = updateAgentCommand.City;
            //agent.Email = updateAgentCommand.Email;
            //agent.Phone = updateAgentCommand.Phone;
            //agent.Country = updateAgentCommand.Country;
            //agent.Address = updateAgentCommand.Address;
            //agent.Company = updateAgentCommand.Company;

            agent.UpdatedDate = DateTime.UtcNow;

            await agentRepository.Collection.ReplaceOneAsync(
                doc => doc.Id == agent.Id,
                agent,
                new UpdateOptions { IsUpsert = true });

            return true;
        }

        public async Task<bool> Handle(DeleteAgentCommand deleteAgentCommand, CancellationToken cancellationToken)
        {
            var agent = await queryExecutor.Execute<GetAgentQuery, Agent>(new GetAgentQuery() { AgentId = deleteAgentCommand.AggregateId });

            await agentRepository.DeleteAsync(agent, deleteAgentCommand.AggregateId);

            return true;
        }

        public async Task<bool> Handle(CreateAgentIntigrationEmailAccountCommand createAgentIntigrationEmailAccountCommand, CancellationToken cancellationToken)
        {
            var agent = await queryExecutor.Execute<GetAgentQuery, Agent>(new GetAgentQuery() { AgentId = createAgentIntigrationEmailAccountCommand.AggregateId });

            agent.CreateMailbox();

            agent.IntegrationEmail = await CreateIntigrationEmail(createAgentIntigrationEmailAccountCommand.AgentEmail.Replace("@", "").Replace(" ", ""));

            agent.UpdatedDate = DateTime.UtcNow;

            await agentRepository.Collection.ReplaceOneAsync(
                doc => doc.Id == agent.Id,
                agent,
                new UpdateOptions { IsUpsert = true });

            var filter = Builders<Agent>.Filter.Eq("Id", agent.Id);
            var update = Builders<Agent>.Update
                .Set("IntegrationEmail", agent.IntegrationEmail)
                .CurrentDate("UpdatedDate");

            await agentRepository.Collection
                    .UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });

            return true;
        }

        public async Task<bool> Handle(CreateAgentTypeFormAccountCommand createAgentTypeFormAccount, CancellationToken cancellationToken)
        {
            var agent = await queryExecutor.Execute<GetAgentQuery, Agent>(new GetAgentQuery() { AgentId = createAgentTypeFormAccount.AggregateId });

            //request to type form for a url
            //create a spreadsheet
            //map typeform and spreadsheet through zap

            //agent.AgentTypeForm = new AgentTypeForm { SpreadsheetUrl = "asdxawd", Type = 1, TypeFormUrl = await CreateTypeformUrl(agent) };

            //await agentRepository.UpdateTypeFormAsync(agent);

            return true;
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