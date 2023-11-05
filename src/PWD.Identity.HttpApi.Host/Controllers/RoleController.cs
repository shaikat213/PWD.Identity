using Microsoft.AspNetCore.Mvc;
using PWD.Identity.DtoModels;
using PWD.Identity.InputDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWD.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager _roleManager;

        public RoleController(RoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
        {
            return await _roleManager.GetListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> Get(string id)
        {
            return await _roleManager.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Post([FromBody] RoleInputDto inputDto)
        {
            return await _roleManager.CreateAsync(inputDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDto>> Put(Guid id, [FromBody] RoleInputDto inputDto)
        {
            if (!await IsExists(id))
            {
                return NotFound();
            }
            return await _roleManager.UpdateAsync(inputDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await IsExists(id))
            {
                return NotFound();
            }
            await _roleManager.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> IsExists(Guid id)
        {
            var client = await _roleManager.GetAsync(id.ToString());
            return client != null;
        }
    }
}
