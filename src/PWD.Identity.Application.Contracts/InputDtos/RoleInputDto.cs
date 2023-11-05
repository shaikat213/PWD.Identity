using System;
using System.Collections.Generic;

namespace PWD.Identity.InputDtos
{
    public class RoleInputDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDefault { get; set; }
    }
}
