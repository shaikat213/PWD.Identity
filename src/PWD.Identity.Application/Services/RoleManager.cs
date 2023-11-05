//using Microsoft.AspNetCore.Identity;
using PWD.Identity.DtoModels;
using PWD.Identity.InputDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.Uow;

namespace PWD.Identity
{
    public class RoleManager : DomainService
    {
        private readonly IGuidGenerator _guidGenerator;
        //private readonly IdentityRoleManager _roleRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<IdentityRole> _roleRepository;

        public RoleManager(IGuidGenerator guidGenerator,
                             IUnitOfWorkManager unitOfWorkManager, 
                             IRepository<IdentityRole> roleRepository
                             )
        {
            _guidGenerator = guidGenerator;
            _unitOfWorkManager = unitOfWorkManager;
            this._roleRepository = roleRepository;
        }

        public async Task<RoleDto> CreateAsync(RoleInputDto input)
        {
            var role = await _roleRepository.FindAsync(x=>x.Id == input.Id);
            if(role == null)
            {
                role = await _roleRepository.InsertAsync(new IdentityRole(_guidGenerator.Create(), input.Name) { 
                    IsDefault = input.IsDefault,
                    IsPublic = input.IsPublic
                },
                autoSave: true
                );
            } 

            return this.CreateRoleDto(role);
        }

        public async Task<RoleDto> GetAsync(string id)
        {
            var role = await _roleRepository.GetAsync(x => x.Id == Guid.Parse(id));
            if (role != null)
            {
                return CreateRoleDto(role);
            }
            return null;
        }

        public async Task<RoleDto> GetAsyncByRoleId(string roleId)
        {
            var client = await _roleRepository.GetAsync(x => x.Id == Guid.Parse(roleId));
            if (client != null)
            {
                return this.CreateRoleDto(client);
            }
            return new RoleDto();
        }

        public async Task<int> GetCountAsync()
        {
            return (await _roleRepository.GetListAsync()).Count;
        }

        public async Task<List<RoleDto>> GetListAsync()
        {
            var roleList = new List<RoleDto>();
            var roles = await _roleRepository.GetListAsync();
            foreach (var role in roles)
            {
                roleList.Add(this.CreateRoleDto(role));
            }
            return roleList;
        }

        public async Task<RoleDto> UpdateAsync(RoleInputDto input)
        {
            var role = await _roleRepository.FindAsync(x => x.Id == input.Id);
            if (role != null)
            {
                role = await _roleRepository.UpdateAsync(new IdentityRole(input.Id, input.Name)
                {
                    IsDefault = input.IsDefault,
                    IsPublic = input.IsPublic
                },
                autoSave: true
                );
            }

            return this.CreateRoleDto(role);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _roleRepository.DeleteAsync(x => x.Id == id);
        }

        private RoleDto CreateRoleDto(Volo.Abp.Identity.IdentityRole role)
        {
            var roleDto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                IsDefault = role.IsDefault,
                IsPublic = role.IsPublic
            };
            return roleDto;
        }
    }
}
