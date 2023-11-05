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
    public class ClientController : ControllerBase
    {
        private readonly ClientManager _clientManager;

        public ClientController(ClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDto>>> Get()
        {
            return await _clientManager.GetListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> Get(Guid id)
        {
            return await _clientManager.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ClientDto>> Post([FromBody] ClientInputDto inputDto)
        {
            return await _clientManager.CreateAsync(inputDto);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult<ClientDto>> Put(Guid id, [FromBody] ClientInputDto inputDto)
        //{
        //    if (!await IsExists(id))
        //    {
        //        return NotFound();
        //    }
        //    return await _clientManager.UpdateAsync(inputDto);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!await IsExists(id))
            {
                return NotFound();
            }
            await _clientManager.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> IsExists(Guid id)
        {
            var client = await _clientManager.GetAsync(id);
            return client != null;
        }
    }
}
