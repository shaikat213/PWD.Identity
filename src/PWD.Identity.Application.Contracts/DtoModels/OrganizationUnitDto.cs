using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace PWD.Identity.DtoModels
{
    public class OrganizationUnitDto : EntityDto<Guid>
    {
        public virtual Guid? ParentId { get; set; }
        public virtual Guid? UserId { get; set; }
        public virtual string Code { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string CivilEm { get; set; }
        public virtual List<OrganizationUnitRoleDto> Roles { get;set; }

    }
    public class ColleagueDto : EntityDto<Guid>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
    }
    
    public class UserToOfficeDto 
    {
        public string UserName { get; set; }
        public string OfficeName { get; set; }
        public bool Action { get; set; } = true;
    }
}
