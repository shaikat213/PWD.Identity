using System.Collections.Generic;

namespace PWD.Identity.InputDtos
{
    public class UserInputDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool LockoutEnabled { get; set; }
        //public IEnumerable<CreateClientSecretDto> CreateClientSecrets { get; set; }
        //public IEnumerable<string> Scopes { get; set; }
        //public IEnumerable<string> GrantTypes { get; set; }
        //public IEnumerable<string> Permissions { get; set; }
        //public IEnumerable<string> CorsOrigins { get; set; }
    }
}
