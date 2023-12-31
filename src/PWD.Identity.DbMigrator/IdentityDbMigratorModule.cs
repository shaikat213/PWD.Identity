﻿using PWD.Identity.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace PWD.Identity.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(IdentityEntityFrameworkCoreModule),
        typeof(IdentityApplicationContractsModule)
        )]
    public class IdentityDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
