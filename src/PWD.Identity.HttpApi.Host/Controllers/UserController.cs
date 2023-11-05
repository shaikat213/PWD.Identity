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
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;

        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            return await _userManager.GetListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(Guid id)
        {
            return await _userManager.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserInputDto inputDto)
        {
            return await _userManager.CreateAsync(inputDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Put(Guid id, [FromBody] UserInputDto inputDto)
        {
            if (!await IsExists(id))
            {
                return NotFound();
            }
            return await _userManager.UpdateAsync(inputDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await IsExists(id))
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> IsExists(Guid id)
        {
            var client = await _userManager.GetAsync(id);
            return client != null;
        }
    }
}
