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
    public class ApiResourceController : Controller
    {
        private readonly ApiResourceManager apiResourceManager;

        public ApiResourceController(ApiResourceManager apiResourceManager)
        {
            this.apiResourceManager = apiResourceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiResourceDto>>> Get()
        {
            return await apiResourceManager.GetListAsync();
        }
    }
}
