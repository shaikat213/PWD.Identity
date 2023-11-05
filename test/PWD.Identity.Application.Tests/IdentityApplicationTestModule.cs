using Volo.Abp.Modularity;

namespace PWD.Identity
{
    [DependsOn(
        typeof(IdentityApplicationModule),
        typeof(IdentityDomainTestModule)
        )]
    public class IdentityApplicationTestModule : AbpModule
    {

    }
}