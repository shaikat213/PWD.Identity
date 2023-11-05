using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace PWD.Identity.InputDtos
{
    public class ClientInputDto
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public string Secret { get; set; }
        public string RedirectUri { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public bool RequirePkce { get; set; }
        public bool IsRequireConsent { get; set; }
        public bool RequireClientSecret { get; set; }

        //public CreateClientDto CreateClient { get; set; }
        //public IEnumerable<CreateClientSecretDto> CreateClientSecrets { get; set; }
        //public IEnumerable<string> Scopes { get; set; }
        //public IEnumerable<string> GrantTypes { get; set; }
        //public IEnumerable<string> Permissions { get; set; }
        //public IEnumerable<string> CorsOrigins { get; set; }
    }

    public class CreateClientDto
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public string Secret { get; set; }
        public string RedirectUri { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public bool RequirePkce { get; set; }
        public bool IsRequireConsent { get; set; }
        public bool RequireClientSecret { get; set; }
    }
    public class CreateClientSecretDto
    {
        public string ClientId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime? Expiration { get; set; }
    }

    public class GetClientsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
