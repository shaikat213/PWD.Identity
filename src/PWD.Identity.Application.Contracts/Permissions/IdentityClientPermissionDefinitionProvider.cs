using PWD.Identity.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PWD.Identity.Permissions
{
    public class IdentityClientPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            // var clientGroup = context.AddGroup(IdentityClientPermissions.GroupName, L("Menu:ClientManagement"));
            var clientGroup = context.AddGroup(IdentityClientPermissions.GroupName, L("Permission:IdentityClient"));

            var clientsPermission = clientGroup.AddPermission(IdentityClientPermissions.IdentityClients.Default, L("Permission:Clients"));
            clientsPermission.AddChild(IdentityClientPermissions.IdentityClients.Create, L("Permission:Clients.Create"));
            clientsPermission.AddChild(IdentityClientPermissions.IdentityClients.Edit, L("Permission:Clients.Edit"));
            clientsPermission.AddChild(IdentityClientPermissions.IdentityClients.Delete, L("Permission:Clients.Delete"));

        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }

}



