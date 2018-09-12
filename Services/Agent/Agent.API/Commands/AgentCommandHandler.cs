namespace Agent.Commands
{
    using Agent.Command;
    using Agent.Domain;
    using Agent.EmailCreator;
    using Agent.Repositories;
    using Agent.Services;
    using Agent.TypeFormIntegration;
    using LeadsPlus.BuildingBlocks.EventBus.Abstractions;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    // Regular CommandHandler
    public class AgentCommandHandler
        : IRequestHandler<CreateAgentCommand, bool>, 
        IRequestHandler<UpdateAgentCommand, bool>,
        IRequestHandler<DeleteAgentCommand, bool>,
        IRequestHandler<CreateAgentIntigrationEmailAccountCommand, bool>,
        IRequestHandler<CreateAgentTypeFormAccountCommand, bool>
    {
        private readonly IAgentRepository agentRepository;
        private readonly IEventBus eventBus;
        private readonly IMediator mediator;
        private readonly IIdentityService identityService;

        public AgentCommandHandler(IMediator mediator, IAgentRepository agentRepository, IEventBus eventBus, IIdentityService identityService)
        {
            agentRepository = agentRepository ?? throw new ArgumentNullException(nameof(agentRepository));
            eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            
            identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> Handle(CreateAgentCommand createAgentCommand, CancellationToken cancellationToken)
        {
            var agent = new Domain.Agent()
            {
                Id = createAgentCommand.AggregateId,
                CreatedBy = createAgentCommand.UserId,
                Firstname = createAgentCommand.Firstname,
                Lastname = createAgentCommand.Lastname,
                City = createAgentCommand.City,
                Email = createAgentCommand.Email,
                Phone = createAgentCommand.Phone,
                Country = createAgentCommand.Country,
                Address = createAgentCommand.Address,
                Company = createAgentCommand.Company,
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            await agentRepository.AddAsync(agent);

            return true;
        }

        public async Task<bool> Handle(UpdateAgentCommand updateAgentCommand, CancellationToken cancellationToken)
        {
            var agent = await agentRepository.GetAsync(updateAgentCommand.AggregateId);

            agent.Firstname = updateAgentCommand.Firstname;
            agent.Lastname = updateAgentCommand.Lastname;
            agent.City = updateAgentCommand.City;
            agent.Email = updateAgentCommand.Email;
            agent.Phone = updateAgentCommand.Phone;
            agent.Country = updateAgentCommand.Country;
            agent.Address = updateAgentCommand.Address;
            agent.Company = updateAgentCommand.Company;

            agent.UpdatedDate = DateTime.UtcNow;

            await agentRepository.UpdateAsync(agent);

            return true;
        }

        public async Task<bool> Handle(DeleteAgentCommand deleteAgentCommand, CancellationToken cancellationToken)
        {
            await agentRepository.DeleteAsync(deleteAgentCommand.AggregateId);

            return true;
        }

        public async Task<bool> Handle(CreateAgentIntigrationEmailAccountCommand createAgentIntigrationEmailAccountCommand, CancellationToken cancellationToken)
        {
            var agent = await agentRepository.GetAsync(createAgentIntigrationEmailAccountCommand.AggregateId);

            agent.IntegrationEmail = await CreateIntigrationEmail(createAgentIntigrationEmailAccountCommand.AgentEmail.Replace("@", "").Replace(" ", ""));

            await agentRepository.UpdateIntigrationEmail(agent);

            return true;
        }

        public async Task<bool> Handle(CreateAgentTypeFormAccountCommand createAgentTypeFormAccount, CancellationToken cancellationToken)
        {
            var agent = await agentRepository.GetAsync(createAgentTypeFormAccount.AggregateId);

            //request to type form for a url
            //create a spreadsheet
            //map typeform and spreadsheet through zap

            agent.AgentTypeForm = new AgentTypeForm { SpreadsheetUrl = "asdxawd", Type = 1, TypeFormUrl = await CreateTypeformUrl(agent) };

            await agentRepository.UpdateTypeFormAsync(agent);

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

    }
}