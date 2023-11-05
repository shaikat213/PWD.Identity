using PWD.Identity.DtoModels;
using PWD.Identity.InputDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace PWD.Identity.Interfaces
{
    public interface IClientAppService
    {
        Task<int> GetCountAsync();
        Task<ClientDto> GetAsync(Guid id);
        Task<ClientDto> GetByClientIdAsync(string clientId);
        Task<List<ClientDto>> GetListAsync();
        Task<ClientDto> CreateAsync(ClientInputDto input);
        Task<ClientDto> UpdateAsync(ClientInputDto ClientDto);
        Task DeleteAsync(Guid id);

        Task<PagedResultDto<ClientDto>> getList(GetClientsInput clientsInput);
    }
}
