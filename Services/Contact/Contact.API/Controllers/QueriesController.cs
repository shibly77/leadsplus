﻿namespace Contact.API.Controllers
{
    using Contact.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Domain;
    using Projection;
    using Contact.Projection.Query;

    [Route("api/v1/[controller]")]
    [Authorize]
    public class QueriesController : ControllerBase
    {
        private readonly IQueryExecutor queryExecutor;
        private readonly IIdentityService identityService;

        public QueriesController(IQueryExecutor queryExecutor)
        {
            queryExecutor = queryExecutor ?? throw new ArgumentNullException(nameof(queryExecutor));
            identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpGet]
        public async Task<IActionResult> GetContact(GetContactQuery query)
        {
            var contact = await queryExecutor.Execute<GetContactQuery, Contact>(query);

            return Ok(contact);
        }
    }
}
