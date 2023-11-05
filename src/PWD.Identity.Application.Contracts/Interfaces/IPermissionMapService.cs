using PWD.Identity.DtoModels;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace PWD.Identity.Interfaces
{
    public interface IPermissionMapService : IApplicationService, IRemoteService
    {
        Task<PermissionDto> GetAsync(string providerName, string[] providerKeys, string permissionGroupKey);
        Task<bool> IsGranted(string providerName, string[] providerKeys, string permissionKey, string permissionGroupKey);
    }
}
