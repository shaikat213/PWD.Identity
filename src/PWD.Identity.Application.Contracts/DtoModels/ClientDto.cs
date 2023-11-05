using System;
using System.Collections.Generic;

namespace PWD.Identity.DtoModels
{
    public class ClientDto
    {
        public Guid Id { get; set; }
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
        public bool RequireConsent { get; set; }
        public bool RequireClientSecret { get; set; }
        public IEnumerable<string> Scopes { get; set; }
        public IEnumerable<string> GrantTypes { get; set; }
        public IEnumerable<string> Permissions { get; set; }
        public IEnumerable<string> CorsOrigins { get; set; }
        public string CorsOrigin { get; set; }
        public string ProtocolType { get; set; }
        public bool Enabled { get; set; }
    }
}
