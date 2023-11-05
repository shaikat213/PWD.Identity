using PWD.Identity.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.IdentityServer.IdentityResources;

namespace PWD.Identity.Services
{
    public class IdentityResourceManager : DomainService
    {
        private readonly IIdentityResourceRepository resourceRepository;

        public IdentityResourceManager(IIdentityResourceRepository resourceRepository)
        {
            this.resourceRepository = resourceRepository;
        }

        public async Task<List<IdentityResourceDto>> GetListAsync()
        {
            var identityResources = await resourceRepository.GetListAsync();

            var identityResourceList = identityResources.ConvertAll(x => new IdentityResourceDto 
            { 
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                DisplayName = x.DisplayName
            });
            return identityResourceList;
        }
    }
}
