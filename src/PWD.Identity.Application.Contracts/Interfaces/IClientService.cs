using PWD.Identity.DtoModels;
using PWD.Identity.InputDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PWD.Identity.Interfaces
{
    public interface IClientService
    {
        Task<int> GetCountAsync();
        Task<ClientDto> GetAsync(Guid id);
        Task<ClientDto> GetAsyncByClientId(string clientId);
        Task<List<ClientDto>> GetListAsync();
        Task<ClientDto> CreateAsync(ClientInputDto input);
        Task<ClientDto> UpdateAsync(ClientInputDto input);
        Task DeleteAsync(Guid id);
    }
}
