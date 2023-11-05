using AutoMapper;
using IdentityServer4.Models;
using PWD.Identity.DtoModels;
using PWD.Identity.InputDtos;
using PWD.Identity.Models;
using Volo.Abp.Identity;

namespace PWD.Identity
{
    public class IdentityApplicationAutoMapperProfile : Profile
    {
        public IdentityApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();
            CreateMap<ClientInputDto, Client>();

            CreateMap<IdentityRole, RoleDto>();
            CreateMap<RoleDto, IdentityRole>();
            CreateMap<RoleInputDto, IdentityRole>();
            
            CreateMap<OrganizationUnit, OrganizationUnitDto>();
            CreateMap<OrganizationUnitDto, OrganizationUnit>();
            
            CreateMap<IdentityUser, ColleagueDto>();
            CreateMap<IdentityUser, ColleagueDto>();
            CreateMap<IdentityUser, IdentityUserDto>();
            
            CreateMap<Posting, PostingDto>();
            CreateMap<OrganizationUnitRole, OrganizationUnitRoleDto>();
            
        }
    }
}
