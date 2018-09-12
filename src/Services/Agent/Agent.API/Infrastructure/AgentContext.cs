namespace Agent.Database.Context
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Agent.Domain;

    public class AgentContext
    {
        private readonly IMongoDatabase _database = null;

        public AgentContext(IOptions<AgentSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Agent> Agents
        {
            get
            {
                return _database.GetCollection<Agent>("Agents");
            }
        }
    }
}
