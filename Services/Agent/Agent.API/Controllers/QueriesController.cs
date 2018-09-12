namespace Agent.API.Controllers
{
    using Agent.Command;
    using Agent.Repositories;
    using Agent.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Agent.Domain;

    [Route("api/v1/[controller]")]
    //[Authorize]
    public class QueriesController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IAgentRepository _agentRepository;

        public QueriesController(IAgentRepository agentRepository, 
            IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _agentRepository = agentRepository ?? throw new ArgumentNullException(nameof(agentRepository));
        }

        //GET api/v1/[controller]/1
        [Route("getagentbyid")]
        [HttpGet]
        [ProducesResponseType(typeof(Agent), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAgent(string id)
        {
            var agnet = await _agentRepository.GetAsync(id);

            if (agnet is null)
            {
                return NotFound();
            }

            return Ok(agnet);
        }

        //GET api/v1/[controller]/
        [Route("getallagent")]
        [HttpGet]
        //[ProducesResponseType(typeof(List<Agent>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAgents()
        {
            var agents = await _agentRepository.GetListAsync();
            return Ok(agents);
        }
    }
}
