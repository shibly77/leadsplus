namespace Agent.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Agent.Domain;

    public interface IAgentRepository
    {        
        Task<Agent> GetAsync(string id);

        Task<Agent> GetByIntegrationEmailAsync(string integrationEmail);

        Task<List<Agent>> GetListAsync();

        Task AddAsync(Agent agent);

        Task DeleteAsync(string Id);

        Task UpdateAsync(Agent agent);

        Task UpdateTypeFormAsync(Agent agent);

        Task UpdateIntigrationEmail(Agent agent);
    }
}
