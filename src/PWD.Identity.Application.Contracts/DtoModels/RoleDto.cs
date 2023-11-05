using System;
using System.Collections.Generic;

namespace PWD.Identity.DtoModels
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDefault { get; set; }
    }
public class OrganizationUnitRoleDto
    {
        public virtual Guid RoleId { get; protected set; }
        public virtual Guid OrganizationUnitId { get; protected set; }

    }
}
