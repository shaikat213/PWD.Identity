using PWD.Identity.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.IdentityServer.ApiResources;

namespace PWD.Identity.Services
{
    public class ApiResourceManager : DomainService
    {
        private readonly IApiResourceRepository apiResourceRepository;

        public ApiResourceManager(IApiResourceRepository apiResourceRepository)
        {
            this.apiResourceRepository = apiResourceRepository;
        }

        public async Task<List<ApiResourceDto>> GetListAsync()
        {
            var apiResources = await apiResourceRepository.GetListAsync();

            var apiResourceList = apiResources.ConvertAll(x => new ApiResourceDto
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                DisplayName = x.DisplayName
            });
            return apiResourceList;
        }
    }
}
