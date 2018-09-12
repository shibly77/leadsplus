namespace Agent.Database.Context
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;
    using Agent.Domain;

    public class AgentContextSeed
    {
        private static AgentContext ctx;
        public static async Task SeedAsync(IApplicationBuilder applicationBuilder, ILoggerFactory loggerFactory)
        {
            var config = applicationBuilder
                .ApplicationServices.GetRequiredService<IOptions<AgentSettings>>();

            ctx = new AgentContext(config);

            if (!ctx.Agents.Database.GetCollection<Agent>(nameof(Agent)).AsQueryable().Any())
            {
                //await SetIndexes();
                await SetAgent();
            }
        }

        static async Task SetAgent()
        {
            var agent = new Agent()
            {
                //Id = Guid.NewGuid().ToString(),
                CreatedBy = "Ilias",
                Firstname = "Ilias",
                Lastname = "Hossain",
                City = "Dhaka",
                Email = "shimulsays@gmail.com",
                Phone = "8801717055870",
                Country = "Bangladesh",
                Address = "8/A/10, Sobahanbug Dhanmondi",
                Company = "Samurai",
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            await ctx.Agents.InsertOneAsync(agent);

            var agent2 = new Agent()
            {
                //Id = Guid.NewGuid().ToString(),
                CreatedBy = "Ilias",
                Firstname = "Andre",
                Lastname = "Hegge",
                City = "Gothenburg",
                Email = "andre@gmail.com",
                Phone = "8801717055870",
                Country = "Sweden",
                Address = "Valhamra",
                Company = "Adfenix",
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            await ctx.Agents.InsertOneAsync(agent2);

            var agent3 = new Agent()
            {
                //Id = Guid.NewGuid().ToString(),
                CreatedBy = "Ilias",
                Firstname = "Gabriel",
                Lastname = "Kaminey",
                City = "Gothenburg",
                Email = "gabriel@gmail.com",
                Phone = "8801717055870",
                Country = "Sweden",
                Address = "Valhamra",
                Company = "Adfenix",
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow
            };

            await ctx.Agents.InsertOneAsync(agent3);
        }

        //static async Task SetIndexes()
        //{
            
        //}
    }
}
