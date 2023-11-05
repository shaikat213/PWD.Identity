using PWD.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PWD.Identity
{
    [DependsOn(
        typeof(IdentityEntityFrameworkCoreTestModule)
        )]
    public class IdentityDomainTestModule : AbpModule
    {

    }
}