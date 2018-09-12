namespace Agent.API.Controllers
{
    using Agent.Domain;
    using Agent.Domain.Query;
    using LeadsPlus.Core.Query;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    [Route("api/v1/[controller]")]
    //[Authorize]
    public class QueriesController : ControllerBase
    {
        private readonly IQueryExecutor queryExecutor;

        public QueriesController(IQueryExecutor queryExecutor)
        {
            this.queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
        }

        //GET api/v1/[controller]/1
        [Route("getagentbyid")]
        [HttpGet]
        [ProducesResponseType(typeof(Agent), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAgent(GetAgentQuery query)
        {
            var agent = await queryExecutor.Execute<GetAgentQuery, Agent>(query);

            if (agent is null)
            {
                return NotFound();
            }

            return Ok(agent);
        }

        //GET api/v1/[controller]/
        [Route("getallagent")]
        [HttpGet]
        //[ProducesResponseType(typeof(List<Agent>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAgents()
        {
            var agents = await queryExecutor.Execute<GetAllAgentQuery, Agent>(new GetAllAgentQuery());

            return Ok(agents);
        }
    }
}
