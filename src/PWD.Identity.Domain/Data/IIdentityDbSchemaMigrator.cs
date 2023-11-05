using System.Threading.Tasks;

namespace PWD.Identity.Data
{
    public interface IIdentityDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
