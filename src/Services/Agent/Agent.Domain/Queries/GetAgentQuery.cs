namespace Agent.Domain.Query
{
    using Contact.Domain;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using System.Threading.Tasks;
    using LeadsPlus.Core.Query;
    using LeadsPlus.Core;

    public class GetAgentQuery
    {
        public string AgentId { get; set; }
    }

    public class GetContactQueryHandler : IQueryHandler<GetAgentQuery, Agent>
    {
        private readonly IRepository<Agent> contactRepository;

        public GetContactQueryHandler(IRepository<Agent> contactQueries)
        {
            this.contactRepository = contactQueries;
        }

        public async Task<Agent> Handle(GetAgentQuery query)
        {
            var filter = Builders<Agent>.Filter.Eq("Id", query.AgentId);

            return await contactRepository.Collection
                                .Find(filter)
                                .FirstOrDefaultAsync();
        }
    }
}
