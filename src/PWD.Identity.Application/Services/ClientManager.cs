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
    public class ClientManager : DomainService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public ClientManager(IGuidGenerator guidGenerator,
                             IClientRepository clientRepository,
                             IUnitOfWorkManager unitOfWorkManager)
        {
            _guidGenerator = guidGenerator;
            _clientRepository = clientRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<ClientDto> CreateAsync(ClientInputDto input)
        {
            var client = await _clientRepository.FindByClientIdAsync(input.CreateClient.ClientId);
            if (client == null)
            {
                client = await _clientRepository.InsertAsync(
                    new Client(
                        _guidGenerator.Create(),
                        input.CreateClient.ClientId
                    )
                    {
                        ClientName = input.CreateClient.ClientName,
                        ProtocolType = "oidc",
                        Description = input.CreateClient.Description,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowOfflineAccess = true,
                        AbsoluteRefreshTokenLifetime = 31536000, //365 days
                        AccessTokenLifetime = 31536000, //365 days
                        AuthorizationCodeLifetime = 300,
                        IdentityTokenLifetime = 300,
                        RequireConsent = input.CreateClient.IsRequireConsent, // false,
                        FrontChannelLogoutUri = input.CreateClient.FrontChannelLogoutUri, // frontChannelLogoutUri,
                        RequireClientSecret = input.CreateClient.RequireClientSecret, // requireClientSecret,
                        RequirePkce = input.CreateClient.RequirePkce, // requirePkce
                        ClientUri = input.CreateClient.ClientUri
                    },
                    autoSave: true
                );
            }

            if (input.CreateClientSecrets != null)
            {
                foreach (var item in input.CreateClientSecrets)
                {
                    client.AddSecret(item.Value, item.Expiration, item.Type, item.Description);
                }
            }

            if (input.Scopes != null)
            {
                input.Scopes.ToList().ForEach(x => client.AddScope(x));
            }

            //foreach (var scope in IdentityCommonScopes.CmmonScopes)
            //{
            //    if (client.FindScope(scope) == null)
            //    {
            //        client.AddScope(scope);
            //    }
            //}

            //foreach (var grantType in IdentityCommonGrantTypes.GrantTypes)
            //{
            //    if (client.FindGrantType(grantType) == null)
            //    {
            //        client.AddGrantType(grantType);
            //    }
            //}

            //if (!input.Secret.IsNullOrEmpty())
            //{
            //    if (client.FindSecret(input.Secret) == null)
            //    {
            //        client.AddSecret(input.Secret);
            //    }
            //}

            //if (input.RedirectUri != null)
            //{
            //    if (client.FindRedirectUri(input.RedirectUri) == null)
            //    {
            //        client.AddRedirectUri(input.RedirectUri);
            //    }
            //}

            //if (input.PostLogoutRedirectUri != null)
            //{
            //    if (client.FindPostLogoutRedirectUri(input.PostLogoutRedirectUri) == null)
            //    {
            //        client.AddPostLogoutRedirectUri(input.PostLogoutRedirectUri);
            //    }
            //}

            ////if (input.Permissions != null)
            ////{
            ////    await _permissionDataSeeder.SeedAsync(ClientPermissionValueProvider.ProviderName,
            ////                                          name,
            ////                                          permissions,
            ////                                          null);
            ////}

            //if (input.CorsOrigins != null)
            //{
            //    foreach (var origin in input.CorsOrigins)
            //    {
            //        if (!origin.IsNullOrWhiteSpace() && client.FindCorsOrigin(origin) == null)
            //        {
            //            client.AddCorsOrigin(origin);
            //        }
            //    }
            //}

            //return await _clientRepository.UpdateAsync(client);
            //  await _clientRepository.UpdateAsync(client);

            return this.CreateClientDto(client);
        }

        public async Task<ClientDto> GetAsync(Guid id)
        {
            var client = await _clientRepository.FindAsync(id);
            if(client != null)
            {
                return CreateClientDto(client);
            }
            return null;
        }

        public async Task<ClientDto> GetAsyncByClientId(string clientId)
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
            foreach(var client in clients)
            {
                clientList.Add(this.CreateClientDto(client));
            }
            return clientList;
        }

        //public async Task<ClientDto> UpdateAsync(ClientInputDto input)
        //{
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
        //}

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
                Enabled = client.Enabled
                //Secret = client.ClientSecrets,
                //RedirectUri = client.RedirectUris,
                //PostLogoutRedirectUri = client.PostLogoutRedirectUris,
                //FrontChannelLogoutUri = client.FrontChannelLogoutUri,
                //Scopes = client.AllowedScopes,
                //GrantTypes = client.AllowedGrantTypes,
                //Permissions = client
                //CorsOrigins = client.AllowedCorsOrigins

            };
            return clientDto;
        }

        //public async Task<ClientDto> CreateAsync(ProjectInputDto input)
        //{
        //    var newProject = ObjectMapper.Map<ProjectInputDto, Client>(input);

        //    var project = await _repository.InsertAsync(newProject);

        //    await _unitOfWorkManager.Current.SaveChangesAsync();

        //    return ObjectMapper.Map<Client, ClientDto>(project);
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    await _repository.DeleteAsync(id);

        //}

        //public async Task<ClientDto> GetAsync(int id)
        //{
        //    var project = await _repository.FindAsync(id);

        //    return ObjectMapper.Map<Client, ClientDto>(project);
        //}

        //public async Task<int> GetCountAsync()
        //{
        //    return (await _repository.GetListAsync()).Count;
        //}

        //public async Task<List<ClientDto>> GetListAsync()
        //{
        //    var projects = await _repository.GetListAsync();

        //    return ObjectMapper.Map<List<Client>, List<ClientDto>>(projects);
        //}

        //public async Task<List<ClientDto>> GetSortedListAsync(FilterModel filterModel)
        //{
        //    var projects = (await _repository.GetListAsync()).Skip(filterModel.Offset).Take(filterModel.Limit).ToList();

        //    return ObjectMapper.Map<List<Client>, List<ClientDto>>(projects);
        //}

        //public async Task<ClientDto> UpdateAsync(ProjectInputDto input)
        //{
        //    var updateProject = ObjectMapper.Map<ProjectInputDto, Client>(input);

        //    var project = await _repository.UpdateAsync(updateProject);

        //    await _unitOfWorkManager.Current.SaveChangesAsync();

        //    return ObjectMapper.Map<Client, ClientDto>(project);
        //}
    }
}
