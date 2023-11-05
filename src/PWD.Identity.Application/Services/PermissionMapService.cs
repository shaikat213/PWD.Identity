using Microsoft.Extensions.Options;
using PWD.Identity.DtoModels;
using PWD.Identity.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SimpleStateChecking;

namespace PWD.Identity
{
    public class PermissionMapService : ApplicationService, IPermissionMapService
    {
        protected PermissionManagementOptions Options { get; }
        protected IPermissionManager PermissionManager { get; }
        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }
        protected ISimpleStateCheckerManager<PermissionDefinition> SimpleStateCheckerManager { get; }

        public PermissionMapService(IPermissionManager permissionManager,
                                    IPermissionDefinitionManager permissionDefinitionManager,
                                    IOptions<PermissionManagementOptions> options,
                                    ISimpleStateCheckerManager<PermissionDefinition> simpleStateCheckerManager)
        {
            Options = options.Value;
            PermissionManager = permissionManager;
            PermissionDefinitionManager = permissionDefinitionManager;
            SimpleStateCheckerManager = simpleStateCheckerManager;
        }

        public virtual async Task<bool> IsGranted(string providerName, string[] providerKeys, string permissionKey, string permissionGroupKey)
        {
            var permissionDto = await GetAsync(providerName, providerKeys, permissionGroupKey);
            permissionDto.Permissions.TryGetValue(permissionKey, out bool isGranted);
            return isGranted;
        }

        public virtual async Task<PermissionDto> GetAsync(string providerName, string[] providerKeys, string permissionGroupKey)
        {
            var permissionDto = new PermissionDto();
            var grantedPermissionDic = new Dictionary<string, bool>();

            var multiTenancySide = CurrentTenant.GetMultiTenancySide();

            foreach (var group in PermissionDefinitionManager.GetGroups())
            {
                var groupDto = new PermissionGroupDto
                {
                    Name = group.Name,
                    DisplayName = group.DisplayName.Localize(StringLocalizerFactory),
                    Permissions = new List<PermissionGrantInfoDto>()
                };

                var neededCheckPermissions = new List<PermissionDefinition>();

                foreach (var permission in group.GetPermissionsWithChildren()
                                                .Where(x => x.IsEnabled)
                                                .Where(x => !x.Providers.Any() || x.Providers.Contains(providerName))
                                                .Where(x => x.MultiTenancySide.HasFlag(multiTenancySide)))
                {
                    if (await SimpleStateCheckerManager.IsEnabledAsync(permission))
                    {
                        neededCheckPermissions.Add(permission);
                    }
                }

                if (!neededCheckPermissions.Any())
                {
                    continue;
                }

                var grantInfoDtos = neededCheckPermissions.Select(x => new PermissionGrantInfoDto
                {
                    Name = x.Name,
                    DisplayName = x.DisplayName.Localize(StringLocalizerFactory),
                    ParentName = x.Parent?.Name,
                    AllowedProviders = x.Providers,
                    GrantedProviders = new List<ProviderInfoDto>()
                }).ToList();

                var providerKeyList = providerKeys[0].Split(',');
                foreach (var providerKey in providerKeyList)
                {
                    var multipleGrantInfo = await PermissionManager.GetAsync(neededCheckPermissions.Select(x => x.Name).ToArray(), providerName, providerKey);

                    foreach (var grantInfo in multipleGrantInfo.Result)
                    {
                        if (grantInfo.IsGranted && grantInfo.Name.Contains(permissionGroupKey))
                        {
                            if (!grantedPermissionDic.ContainsKey(grantInfo.Name))
                            {
                                grantedPermissionDic.Add(grantInfo.Name, grantInfo.IsGranted);
                            }
                        }
                    }
                }
            }

            permissionDto.Permissions = grantedPermissionDic;

            return permissionDto;
        }
    }
}
