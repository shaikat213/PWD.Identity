using IdentityServer4.Models;
using PWD.Identity.DtoModels;
using PWD.Identity.InputDtos;
using PWD.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Guids;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using Volo.Abp.Domain.Repositories;

using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using Client = Volo.Abp.IdentityServer.Clients.Client;
using System.Security.Cryptography.X509Certificates;

namespace PWD.Identity
{
    public class ClientAppService : ApplicationService, IClientAppService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IApiScopeRepository _apiScopeRepository;
        private readonly IPermissionDataSeeder _permissionDataSeeder;
        private readonly IApiResourceRepository _apiResourceRepository;

        public ClientAppService(IGuidGenerator guidGenerator,
                                IClientRepository clientRepository,
                                IUnitOfWorkManager unitOfWorkManager,
                                IApiScopeRepository apiScopeRepository,
                                IPermissionDataSeeder permissionDataSeeder,
                                IApiResourceRepository apiResourceRepository)
        {
            _guidGenerator = guidGenerator;
            _clientRepository = clientRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _apiScopeRepository = apiScopeRepository;
            _permissionDataSeeder = permissionDataSeeder;
            _apiResourceRepository = apiResourceRepository;
        }

        public async Task<ClientDto> CreateAsync(ClientInputDto input)
        {
            await CreateApiResourcesAsync(input);
            await CreateApiScopesAsync(input);
            return await CreateClientsAsync(input);
        }

        public async Task<ClientDto> GetAsync(Guid id)
        {
            var client = await _clientRepository.FindAsync(id);
            if (client != null)
            {
                return CreateClientDto(client);
            }
            return null;
        }

        public async Task<ClientDto> GetByClientIdAsync(string clientId)
        {
            var client = await _clientRepository.FindByClientIdAsync(clientId);
            if (client != null)
            {
                return this.CreateClientDto(client);
            }
            return new ClientDto();
        }

        public async Task<int> GetCountAsync()
        {
            return (await _clientRepository.GetListAsync()).Count;
        }

        public async Task<List<ClientDto>> GetListAsync()
        {
            var clientList = new List<ClientDto>();
            var clients = await _clientRepository.GetListAsync();
            foreach (var client in clients)
            {
                clientList.Add(this.CreateClientDto(client));
            }
            return clientList;
        }

        public Task<ClientDto> UpdateAsync(ClientInputDto input)
        {
            throw new NotImplementedException();

            //    var client = await _clientRepository.FindByClientIdAsync(input.ClientId);
            //    if (client != null)
            //    {
            //        client.ClientName = input.ClientName;
            //        client.ProtocolType = "oidc";
            //        client.Description = input.Description;
            //        client.AlwaysIncludeUserClaimsInIdToken = true;
            //        client.AllowOfflineAccess = true;
            //        client.AbsoluteRefreshTokenLifetime = 31536000; //365 days
            //        client.AccessTokenLifetime = 31536000; //365 days
            //        client.AuthorizationCodeLifetime = 300;
            //        client.IdentityTokenLifetime = 300;
            //        client.RequireConsent = input.IsRequireConsent; // false;
            //        client.FrontChannelLogoutUri = input.FrontChannelLogoutUri; // frontChannelLogoutUri;
            //        client.RequireClientSecret = input.RequireClientSecret; // requireClientSecret;
            //        client.RequirePkce = input.RequirePkce; // requirePkce
            //    }

            //    foreach (var scope in IdentityCommonScopes.CmmonScopes)
            //    {
            //        if (client.FindScope(scope) == null)
            //        {
            //            client.AddScope(scope);
            //        }
            //    }

            //    foreach (var grantType in IdentityCommonGrantTypes.GrantTypes)
            //    {
            //        if (client.FindGrantType(grantType) == null)
            //        {
            //            client.AddGrantType(grantType);
            //        }
            //    }

            //    if (!input.Secret.IsNullOrEmpty())
            //    {
            //        if (client.FindSecret(input.Secret) == null)
            //        {
            //            client.AddSecret(input.Secret);
            //        }
            //    }

            //    if (input.RedirectUri != null)
            //    {
            //        if (client.FindRedirectUri(input.RedirectUri) == null)
            //        {
            //            client.AddRedirectUri(input.RedirectUri);
            //        }
            //    }

            //    if (input.PostLogoutRedirectUri != null)
            //    {
            //        if (client.FindPostLogoutRedirectUri(input.PostLogoutRedirectUri) == null)
            //        {
            //            client.AddPostLogoutRedirectUri(input.PostLogoutRedirectUri);
            //        }
            //    }

            //    //if (input.Permissions != null)
            //    //{
            //    //    await _permissionDataSeeder.SeedAsync(ClientPermissionValueProvider.ProviderName,
            //    //                                          name,
            //    //                                          permissions,
            //    //                                          null);
            //    //}

            //    if (input.CorsOrigins != null)
            //    {
            //        foreach (var origin in input.CorsOrigins)
            //        {
            //            if (!origin.IsNullOrWhiteSpace() && client.FindCorsOrigin(origin) == null)
            //            {
            //                client.AddCorsOrigin(origin);
            //            }
            //        }
            //    }

            //    await _clientRepository.UpdateAsync(client);

            //    return this.CreateClientDto(client);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _clientRepository.DeleteAsync(id);
        }

        private ClientDto CreateClientDto(Client client)
        {
            var clientDto = new ClientDto
            {
                Id = client.Id,
                ClientId = client.ClientId,
                ClientName = client.ClientName,
                Description = client.Description,
                ClientUri = client.ClientUri,
                LogoUri = client.LogoUri,
                RequirePkce = client.RequirePkce,
                RequireConsent = client.RequireConsent,
                RequireClientSecret = client.RequireClientSecret,
                ProtocolType = client.ProtocolType,
                Enabled = client.Enabled,
                Secret = client.ClientSecrets?.Select(cs => cs.Value).FirstOrDefault(),
                RedirectUri = client.RedirectUris?.Select(ru => ru.RedirectUri).FirstOrDefault(),
                PostLogoutRedirectUri = client.PostLogoutRedirectUris?.Select(ru => ru.PostLogoutRedirectUri).FirstOrDefault(),
                FrontChannelLogoutUri = client.FrontChannelLogoutUri,
                CorsOrigin = client.AllowedCorsOrigins?.Select(aco => aco.Origin).FirstOrDefault()
                //Scopes = client.AllowedScopes,
                //GrantTypes = client.AllowedGrantTypes,
                //Permissions = client
                //CorsOrigins = client.AllowedCorsOrigins

            };
            return clientDto;
        }

        public async Task<PagedResultDto<ClientDto>> getList(GetClientsInput input)
        {
            var clientList = new List<ClientDto>();
            //var clients = await _clientRepository.GetListAsync(includeDetails: true);
            //var clients = await _clientRepository.GetPagedListAsync(skipCount: input.SkipCount, 
            //                                                        maxResultCount: input.MaxResultCount, 
            //                                                        sorting: input.Sorting, 
            //                                                        includeDetails: true);

            var clients = await _clientRepository.GetListAsync(sorting: input.Sorting,
                                                               skipCount: input.SkipCount,
                                                               maxResultCount: input.MaxResultCount,
                                                               filter: input.Filter,
                                                               includeDetails: true);
            foreach (var client in clients)
            {
                clientList.Add(this.CreateClientDto(client));
            }

            return new PagedResultDto<ClientDto>
            {
                TotalCount = await GetCountAsync(),
                Items = clientList
            };
        }

        private async Task CreateApiScopesAsync(ClientInputDto input)
        {
            await CreateApiScopeAsync(GetScoppedClient(input.ClientName));
        }

        private async Task CreateApiResourcesAsync(ClientInputDto input)
        {
            await CreateApiResourceAsync(GetScoppedClient(input.ClientName), IdentityUtility.ApiUserClaims);
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

        private async Task<ClientDto> CreateClientsAsync(ClientInputDto input)
        {
            if (input.ClientId.Contains("Swagger", StringComparison.OrdinalIgnoreCase))
            {
                return await CreateApiClient(input);
            }
            else
            {
                return await CreateAppClient(input);
            }
        }

        // APP Client
        private async Task<ClientDto> CreateAppClient(ClientInputDto input)
        {
            var clientDto = new ClientDto();
            if (!input.ClientId.IsNullOrWhiteSpace())
            {
                var redirectUri = input.RedirectUri?.TrimEnd('/');

                clientDto = await CreateClientAsync(name: input.ClientId,
                                                    description: input.Description,
                                                    scopes: GetApiScopes(input.ClientName),
                                                    grantTypes: new[] { "password", "client_credentials", "authorization_code" },
                                                    secret: (input.Secret ?? "1q2w3e*").Sha256(),
                                                    requireClientSecret: false,
                                                    redirectUri: redirectUri,
                                                    postLogoutRedirectUri: redirectUri,
                                                    corsOrigins: new[] { redirectUri.RemovePostFix("/") });
            }
            return clientDto;
        }

        // API Client (Swagger)
        private async Task<ClientDto> CreateApiClient(ClientInputDto input)
        {
            var clientDto = new ClientDto();
            if (!input.ClientId.IsNullOrWhiteSpace())
            {
                var redirectUri = input.RedirectUri.TrimEnd('/');

                clientDto = await CreateClientAsync(name: input.ClientId,
                                                    description: input.Description,
                                                    scopes: GetApiScopes(input.ClientName),
                                                    grantTypes: new[] { "authorization_code" },
                                                    secret: (input.Secret ?? "1q2w3e*").Sha256(),
                                                    requireClientSecret: false,
                                                    redirectUri: $"{redirectUri}/swagger/oauth2-redirect.html",
                                                    corsOrigins: new[] { redirectUri.RemovePostFix("/") });
            }

            return clientDto;
        }

        // API Scopes
        private string[] GetApiScopes(string clientName)
        {
            var apiScopes = new List<string> { GetScoppedClient(clientName) };
            apiScopes.AddRange(IdentityUtility.ApiScopes);
            return apiScopes.ToArray();
        }

        // Scoped Client Name
        private string GetScoppedClient(string clientName)
        {
            if (!string.IsNullOrWhiteSpace(clientName))
            {
                if (clientName.Contains("_") || clientName.Contains("-"))
                {
                    clientName = (clientName.Replace("-", "_")).Split("_")[0];
                }
            }

            return clientName;
        }

        // Create new client
        private async Task<ClientDto> CreateClientAsync(string name,
                                                        string description,
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
                        Description = description ?? name,
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

            //return await _clientRepository.UpdateAsync(client);
            return this.CreateClientDto(client);
        }

        public async Task<string> ClientSting()
        {
            var clients = await _clientRepository.GetListAsync();
            var uris = new List<string>();
            foreach (var c in clients)
            {
                var x = await _clientRepository.GetAsync(c.Id);
                x.RedirectUris?
                    .ForEach(r =>
                    {
                        var uri = r.RedirectUri;
                        if (uri.Contains("swagger"))
                        {
                            uri = uri.Replace("swagger/oauth2-redirect.html", "");
                        }
                        else
                            uri += "/";
                        uris.Add(uri);
                    });
            };
            return string.Join(',', uris);
        }
    }
}
