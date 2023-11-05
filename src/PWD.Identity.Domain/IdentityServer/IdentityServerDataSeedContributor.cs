using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace PWD.Identity.IdentityServer
{
    public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IPermissionDataSeeder _permissionDataSeeder;
        private readonly IApiScopeRepository _apiScopeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IConfiguration _configuration;
        private readonly ICurrentTenant _currentTenant;

        public IdentityServerDataSeedContributor(IIdentityResourceDataSeeder identityResourceDataSeeder,
                                                 IApiResourceRepository apiResourceRepository,
                                                 IPermissionDataSeeder permissionDataSeeder,
                                                 IApiScopeRepository apiScopeRepository,
                                                 IClientRepository clientRepository,
                                                 IGuidGenerator guidGenerator,
                                                 IConfiguration configuration,
                                                 ICurrentTenant currentTenant)
        {
            _identityResourceDataSeeder = identityResourceDataSeeder;
            _apiResourceRepository = apiResourceRepository;
            _permissionDataSeeder = permissionDataSeeder;
            _apiScopeRepository = apiScopeRepository;
            _clientRepository = clientRepository;
            _guidGenerator = guidGenerator;
            _configuration = configuration;
            _currentTenant = currentTenant;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                await _identityResourceDataSeeder.CreateStandardResourcesAsync();
                await CreateApiResourcesAsync();
                await CreateApiScopesAsync();
                await CreateClientsAsync();
            }
        }

        private async Task CreateApiScopesAsync()
        {
            foreach (var clientName in IdentityUtility.ClientNames)
            {
                await CreateApiScopeAsync(clientName);
            }
        }

        private async Task CreateApiResourcesAsync()
        {
            foreach (var clientName in IdentityUtility.ClientNames)
            {
                await CreateApiResourceAsync(clientName, IdentityUtility.ApiUserClaims);
            }
        }

        private async Task<ApiResource> CreateApiResourceAsync(string name, IEnumerable<string> claims)
        {
            var apiResource = await _apiResourceRepository.FindByNameAsync(name);
            if (apiResource == null)
            {
                apiResource = await _apiResourceRepository.InsertAsync(
                    new ApiResource(_guidGenerator.Create(),
                                    name,
                                    name + " API"),
                    autoSave: true
                );
            }

            foreach (var claim in claims)
            {
                if (apiResource.FindClaim(claim) == null)
                {
                    apiResource.AddUserClaim(claim);
                }
            }

            return await _apiResourceRepository.UpdateAsync(apiResource);
        }

        private async Task<ApiScope> CreateApiScopeAsync(string name)
        {
            var apiScope = await _apiScopeRepository.GetByNameAsync(name);
            if (apiScope == null)
            {
                apiScope = await _apiScopeRepository.InsertAsync(
                    new ApiScope(
                        _guidGenerator.Create(),
                        name,
                        name + " API"
                    ),
                    autoSave: true
                );
            }

            return apiScope;
        }

        private async Task CreateClientsAsync()
        {
            var configurationSection = _configuration.GetSection("IdentityServer:Clients");
            foreach (var clientName in IdentityUtility.ClientNames)
            {
                List<string> apiScopes;
                if (clientName.Equals(IdentityUtility.IDENTITY))
                {
                    apiScopes = new List<string> { clientName };
                    apiScopes.AddRange(IdentityUtility.ApiScopes);
                    await CreateIdentityAppClient(apiScopes.ToArray(), configurationSection);
                    await CreateIdentityApiClient(apiScopes.ToArray(), configurationSection);
                }
                else if (clientName.Equals(IdentityUtility.SCHEDULE))
                {
                    apiScopes = new List<string> { clientName };
                    apiScopes.AddRange(IdentityUtility.ApiScopes);
                    await CreateScheduleAppClient(apiScopes.ToArray(), configurationSection);
                    await CreateScheduleApiClient(apiScopes.ToArray(), configurationSection);
                }
                else if (clientName.Equals(IdentityUtility.HR))
                {
                    apiScopes = new List<string> { clientName };
                    apiScopes.AddRange(IdentityUtility.ApiScopes);
                    await CreateHRAppClient(apiScopes.ToArray(), configurationSection);
                    await CreateHRApiClient(apiScopes.ToArray(), configurationSection);
                }
                else if (clientName.Equals(IdentityUtility.PMMS))
                {
                    apiScopes = new List<string> { clientName };
                    apiScopes.AddRange(IdentityUtility.ApiScopes);
                    await CreatePmmsAppClient(apiScopes.ToArray(), configurationSection);
                    await CreatePmmsApiClient(apiScopes.ToArray(), configurationSection);
                }
                else if (clientName.Equals(IdentityUtility.CMS))
                {
                    apiScopes = new List<string> { clientName };
                    apiScopes.AddRange(IdentityUtility.ApiScopes);
                    await CreateCmsAppClient(apiScopes.ToArray(), configurationSection);
                    await CreateCmsApiClient(apiScopes.ToArray(), configurationSection);
                }
                else if (clientName.Equals(IdentityUtility.Dengue))
                {
                    apiScopes = new List<string> { clientName };
                    apiScopes.AddRange(IdentityUtility.ApiScopes);
                    await CreateDengueAppClient(apiScopes.ToArray(), configurationSection);
                    await CreateDengueApiClient(apiScopes.ToArray(), configurationSection);
                }
                else if (clientName.Equals(IdentityUtility.MySqlHR))
                {
                    apiScopes = new List<string> { clientName };
                    apiScopes.AddRange(IdentityUtility.ApiScopes);
                    await CreateMySqlHRApiClient(apiScopes.ToArray(), configurationSection);
                    
                }
                else if (clientName.Equals(IdentityUtility.Template))
                {
                    apiScopes = new List<string> { clientName };
                    apiScopes.AddRange(IdentityUtility.ApiScopes);
                    await CreateTemplateAppClient(apiScopes.ToArray(), configurationSection);
                    await CreateTemplateApiClient(apiScopes.ToArray(), configurationSection);
                }
            }
        }

        // Identity App Client
        private async Task CreateIdentityAppClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var identityClientId = configurationSection["Identity_App:ClientId"];
            if (!identityClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["Identity_App:RootUrl"]?.TrimEnd('/');

                await CreateClientAsync(name: identityClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "password", "client_credentials", "authorization_code" },
                                        secret: (configurationSection["Identity_App:ClientSecret"] ?? "1q2w3e*").Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: webClientRootUrl,
                                        postLogoutRedirectUri: webClientRootUrl,
                                        corsOrigins: new[] { webClientRootUrl.RemovePostFix("/") });
            }
        }

        // Identity Api Client
        private async Task CreateIdentityApiClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var identitySwaggerClientId = configurationSection["Identity_Swagger:ClientId"];
            if (!identitySwaggerClientId.IsNullOrWhiteSpace())
            {
                var swaggerRootUrl = configurationSection["Identity_Swagger:RootUrl"].TrimEnd('/');

                await CreateClientAsync(name: identitySwaggerClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "authorization_code" },
                                        secret: configurationSection["Identity_Swagger:ClientSecret"]?.Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                                        corsOrigins: new[] { swaggerRootUrl.RemovePostFix("/") });
            }
        }

        // Schedule App Client
        private async Task CreateScheduleAppClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var scheduleClientId = configurationSection["Schedule_App:ClientId"];
            if (!scheduleClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["Schedule_App:RootUrl"]?.TrimEnd('/');

                await CreateClientAsync(name: scheduleClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "password", "client_credentials", "authorization_code" },
                                        secret: (configurationSection["Schedule_App:ClientSecret"] ?? "1q2w3e*").Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: webClientRootUrl,
                                        postLogoutRedirectUri: webClientRootUrl,
                                        corsOrigins: new[] { webClientRootUrl.RemovePostFix("/") });
            }
        }

        // Schedule Api Client
        private async Task CreateScheduleApiClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var scheduleSwaggerClientId = configurationSection["Schedule_Swagger:ClientId"];
            if (!scheduleSwaggerClientId.IsNullOrWhiteSpace())
            {
                var swaggerRootUrl = configurationSection["Schedule_Swagger:RootUrl"].TrimEnd('/');

                await CreateClientAsync(name: scheduleSwaggerClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "authorization_code" },
                                        secret: configurationSection["Schedule_Swagger:ClientSecret"]?.Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                                        corsOrigins: new[] { swaggerRootUrl.RemovePostFix("/") });
            }
        }

        // HR App Client
        private async Task CreateHRAppClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var hrClientId = configurationSection["HR_App:ClientId"];
            if (!hrClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["HR_App:RootUrl"]?.TrimEnd('/');

                await CreateClientAsync(name: hrClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "password", "client_credentials", "authorization_code" },
                                        secret: (configurationSection["HR_App:ClientSecret"] ?? "1q2w3e*").Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: webClientRootUrl,
                                        postLogoutRedirectUri: webClientRootUrl,
                                        corsOrigins: new[] { webClientRootUrl.RemovePostFix("/") });
            }
        }

        // HR Api Client
        private async Task CreateHRApiClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var hrSwaggerClientId = configurationSection["HR_Swagger:ClientId"];
            if (!hrSwaggerClientId.IsNullOrWhiteSpace())
            {
                var swaggerRootUrl = configurationSection["HR_Swagger:RootUrl"].TrimEnd('/');

                await CreateClientAsync(name: hrSwaggerClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "authorization_code" },
                                        secret: configurationSection["HR_Swagger:ClientSecret"]?.Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                                        corsOrigins: new[] { swaggerRootUrl.RemovePostFix("/") });
            }
        }

        // PMMS App Client
        private async Task CreatePmmsAppClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var pmmsClientId = configurationSection["PMMS_App:ClientId"];
            if (!pmmsClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["PMMS_App:RootUrl"]?.TrimEnd('/');

                await CreateClientAsync(name: pmmsClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "password", "client_credentials", "authorization_code" },
                                        secret: (configurationSection["PMMS_App:ClientSecret"] ?? "1q2w3e*").Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: webClientRootUrl,
                                        postLogoutRedirectUri: webClientRootUrl,
                                        corsOrigins: new[] { webClientRootUrl.RemovePostFix("/") });
            }
        }

        // PMMS Api Client
        private async Task CreatePmmsApiClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var pmmsSwaggerClientId = configurationSection["PMMS_Swagger:ClientId"];
            if (!pmmsSwaggerClientId.IsNullOrWhiteSpace())
            {
                var swaggerRootUrl = configurationSection["PMMS_Swagger:RootUrl"].TrimEnd('/');

                await CreateClientAsync(name: pmmsSwaggerClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "authorization_code" },
                                        secret: configurationSection["PMMS_Swagger:ClientSecret"]?.Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                                        corsOrigins: new[] { swaggerRootUrl.RemovePostFix("/") });
            }
        }

        // CMS App Client
        private async Task CreateCmsAppClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var cmsClientId = configurationSection["CMS_App:ClientId"];
            if (!cmsClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["CMS_App:RootUrl"]?.TrimEnd('/');

                await CreateClientAsync(name: cmsClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "password", "client_credentials", "authorization_code" },
                                        secret: (configurationSection["CMS_App:ClientSecret"] ?? "1q2w3e*").Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: webClientRootUrl,
                                        postLogoutRedirectUri: webClientRootUrl,
                                        corsOrigins: new[] { webClientRootUrl.RemovePostFix("/") });
            }
        }

        // CMS Api Client
        private async Task CreateCmsApiClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var cmsSwaggerClientId = configurationSection["CMS_Swagger:ClientId"];
            if (!cmsSwaggerClientId.IsNullOrWhiteSpace())
            {
                var swaggerRootUrl = configurationSection["CMS_Swagger:RootUrl"].TrimEnd('/');

                await CreateClientAsync(name: cmsSwaggerClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "authorization_code" },
                                        secret: configurationSection["CMS_Swagger:ClientSecret"]?.Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                                        corsOrigins: new[] { swaggerRootUrl.RemovePostFix("/") });
            }
        }

        // Dengue App Client
        private async Task CreateDengueAppClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var dengueClientId = configurationSection["Dengue_App:ClientId"];
            if (!dengueClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["Dengue_App:RootUrl"]?.TrimEnd('/');

                await CreateClientAsync(name: dengueClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "password", "client_credentials", "authorization_code" },
                                        secret: (configurationSection["Dengue_App:ClientSecret"] ?? "1q2w3e*").Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: webClientRootUrl,
                                        postLogoutRedirectUri: webClientRootUrl,
                                        corsOrigins: new[] { webClientRootUrl.RemovePostFix("/") });
            }
        }

        // Dengue Api Client
        private async Task CreateDengueApiClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var dengueSwaggerClientId = configurationSection["Dengue_Swagger:ClientId"];
            if (!dengueSwaggerClientId.IsNullOrWhiteSpace())
            {
                var swaggerRootUrl = configurationSection["Dengue_Swagger:RootUrl"].TrimEnd('/');

                await CreateClientAsync(name: dengueSwaggerClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "authorization_code" },
                                        secret: configurationSection["Dengue_Swagger:ClientSecret"]?.Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                                        corsOrigins: new[] { swaggerRootUrl.RemovePostFix("/") });
            }
        }

        // MySqlHR Api Client
        private async Task CreateMySqlHRApiClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var mySqlHrApiSwaggerClientId = configurationSection["MySqlHR_Swagger:ClientId"];
            if (!mySqlHrApiSwaggerClientId.IsNullOrWhiteSpace())
            {
                var swaggerRootUrl = configurationSection["MySqlHR_Swagger:RootUrl"].TrimEnd('/');

                await CreateClientAsync(name: mySqlHrApiSwaggerClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "authorization_code" },
                                        secret: configurationSection["MySqlHR_Swagger:ClientSecret"]?.Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                                        corsOrigins: new[] { swaggerRootUrl.RemovePostFix("/") });
            }
        }

        // Template App Client
        private async Task CreateTemplateAppClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var templateClientId = configurationSection["Template_App:ClientId"];
            if (!templateClientId.IsNullOrWhiteSpace())
            {
                var webClientRootUrl = configurationSection["Template_App:RootUrl"]?.TrimEnd('/');

                await CreateClientAsync(name: templateClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "password", "client_credentials", "authorization_code" },
                                        secret: (configurationSection["Template_App:ClientSecret"] ?? "1q2w3e*").Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: webClientRootUrl,
                                        postLogoutRedirectUri: webClientRootUrl,
                                        corsOrigins: new[] { webClientRootUrl.RemovePostFix("/") });
            }
        }

        // Template Api Client
        private async Task CreateTemplateApiClient(string[] commonScopes, IConfigurationSection configurationSection)
        {
            var templateSwaggerClientId = configurationSection["Template_Swagger:ClientId"];
            if (!templateSwaggerClientId.IsNullOrWhiteSpace())
            {
                var swaggerRootUrl = configurationSection["Template_Swagger:RootUrl"].TrimEnd('/');

                await CreateClientAsync(name: templateSwaggerClientId,
                                        scopes: commonScopes,
                                        grantTypes: new[] { "authorization_code" },
                                        secret: configurationSection["Template_Swagger:ClientSecret"]?.Sha256(),
                                        requireClientSecret: false,
                                        redirectUri: $"{swaggerRootUrl}/swagger/oauth2-redirect.html",
                                        corsOrigins: new[] { swaggerRootUrl.RemovePostFix("/") });
            }
        }

        // Create new client
        private async Task<Client> CreateClientAsync(string name,
                                                     IEnumerable<string> scopes,
                                                     IEnumerable<string> grantTypes,
                                                     string secret = null,
                                                     string redirectUri = null,
                                                     string postLogoutRedirectUri = null,
                                                     string frontChannelLogoutUri = null,
                                                     bool requireClientSecret = true,
                                                     bool requirePkce = false,
                                                     IEnumerable<string> permissions = null,
                                                     IEnumerable<string> corsOrigins = null)
        {
            var client = await _clientRepository.FindByClientIdAsync(name);
            if (client == null)
            {
                client = await _clientRepository.InsertAsync(
                    new Client(
                        _guidGenerator.Create(),
                        name
                    )
                    {
                        ClientName = name,
                        ProtocolType = "oidc",
                        Description = name,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowOfflineAccess = true,
                        AbsoluteRefreshTokenLifetime = 31536000, //365 days
                        AccessTokenLifetime = 31536000, //365 days
                        AuthorizationCodeLifetime = 300,
                        IdentityTokenLifetime = 300,
                        RequireConsent = false,
                        FrontChannelLogoutUri = frontChannelLogoutUri,
                        RequireClientSecret = requireClientSecret,
                        RequirePkce = requirePkce
                    },
                    autoSave: true
                );
            }

            foreach (var scope in scopes)
            {
                if (client.FindScope(scope) == null)
                {
                    client.AddScope(scope);
                }
            }

            foreach (var grantType in grantTypes)
            {
                if (client.FindGrantType(grantType) == null)
                {
                    client.AddGrantType(grantType);
                }
            }

            if (!secret.IsNullOrEmpty())
            {
                if (client.FindSecret(secret) == null)
                {
                    client.AddSecret(secret);
                }
            }

            if (redirectUri != null)
            {
                if (client.FindRedirectUri(redirectUri) == null)
                {
                    client.AddRedirectUri(redirectUri);
                }
            }

            if (postLogoutRedirectUri != null)
            {
                if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
                {
                    client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
                }
            }

            if (permissions != null)
            {
                await _permissionDataSeeder.SeedAsync(ClientPermissionValueProvider.ProviderName,
                                                      name,
                                                      permissions,
                                                      null);
            }

            if (corsOrigins != null)
            {
                foreach (var origin in corsOrigins)
                {
                    if (!origin.IsNullOrWhiteSpace() && client.FindCorsOrigin(origin) == null)
                    {
                        client.AddCorsOrigin(origin);
                    }
                }
            }

            return await _clientRepository.UpdateAsync(client);
        }
    }
}