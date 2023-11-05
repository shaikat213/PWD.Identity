using PWD.Identity.DtoModels;
using PWD.Identity.InputDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.Uow;

namespace PWD.Identity
{
    // Need to work here
    public class UserManager : DomainService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UserManager(IGuidGenerator guidGenerator,
                             IClientRepository clientRepository,
                             IUnitOfWorkManager unitOfWorkManager)
        {
            _guidGenerator = guidGenerator;
            _clientRepository = clientRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<UserDto> CreateAsync(UserInputDto input)
        {
            return this.CreateUserDto();
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            var client = await _clientRepository.FindAsync(id);
            if(client != null)
            {
                return CreateUserDto();
            }
            return null;
        }

        public async Task<UserDto> GetAsyncByUserId(string userId)
        {

            //var client = await _clientRepository.FindByUserIdAsync(userId);
            //if (client != null)
            //{
            //    return this.CreateUserDto(client);
            //}
            return new UserDto();
        }

        public async Task<int> GetCountAsync()
        {
            return (await _clientRepository.GetListAsync()).Count;
        }

        public async Task<List<UserDto>> GetListAsync()
        {
            var clientList = new List<UserDto>();
            var clients = await _clientRepository.GetListAsync();
            foreach(var client in clients)
            {
                clientList.Add(this.CreateUserDto());
            }
            return clientList;
        }

        public async Task<UserDto> UpdateAsync(UserInputDto input)
        {
            return this.CreateUserDto();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _clientRepository.DeleteAsync(id);
        }

        private UserDto CreateUserDto()
        {
            var userDto = new UserDto
            {
                
            };
            return userDto;
        }
    }
}
