namespace Agent.Repositories
{
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Agent.Domain;
    using Agent.Database.Context;

    public class AgentRepository
        : IAgentRepository
    {
        private readonly AgentContext _context;       

        public AgentRepository(IOptions<AgentSettings> settings)
        {
            _context = new AgentContext(settings);
        }        
        
        public async Task<Agent> GetAsync(string agentId)
        {
            var filter = Builders<Agent>.Filter.Eq("Id", agentId);

            return await _context.Agents
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task<Agent> GetByIntegrationEmailAsync(string integrationEmail)
        {
            var filter = Builders<Agent>.Filter.Eq("IntegrationEmail", integrationEmail);

            return await _context.Agents
                                 .Find(filter)
                                 .FirstOrDefaultAsync();
        }

        public async Task<List<Agent>> GetListAsync()
        {
            return await _context.Agents.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddAsync(Agent agent)
        {
            await _context.Agents.InsertOneAsync(agent);
        }

        public async Task DeleteAsync(string agentId)
        {
            var filter = Builders<Agent>.Filter.Eq("Id", agentId);

            await _context.Agents.DeleteOneAsync(filter);
        }

        public async Task UpdateAsync(Agent agent)
        {            
            await _context.Agents.ReplaceOneAsync(
                doc => doc.Id == agent.Id,
                agent,
                new UpdateOptions { IsUpsert = true });
        }

        public async Task UpdateTypeFormAsync(Agent agent)
        {
            var filter = Builders<Agent>.Filter.Eq("Id", agent.Id);

            var update = Builders<Agent>.Update
                .Set("AgentTypeForm", agent.AgentTypeForm)
                .CurrentDate("UpdatedDate");

            await _context.Agents
                .UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }

        public async Task UpdateIntigrationEmail(Agent agent)
        {
            var filter = Builders<Agent>.Filter.Eq("Id", agent.Id);
            var update = Builders<Agent>.Update
                .Set("IntegrationEmail", agent.IntegrationEmail)
                .CurrentDate("UpdatedDate");

            await _context.Agents
                .UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
        }
    }
}
