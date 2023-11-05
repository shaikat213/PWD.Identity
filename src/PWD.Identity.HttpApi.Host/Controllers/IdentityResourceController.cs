using Microsoft.AspNetCore.Mvc;
using PWD.Identity.DtoModels;
using PWD.Identity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWD.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityResourceController : ControllerBase
    {
        private readonly IdentityResourceManager identityResourceManager;

        public IdentityResourceController(IdentityResourceManager identityResourceManager)
        {
            this.identityResourceManager = identityResourceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityResourceDto>>> Get()
        {
            return await identityResourceManager.GetListAsync();
        }
    }
}
