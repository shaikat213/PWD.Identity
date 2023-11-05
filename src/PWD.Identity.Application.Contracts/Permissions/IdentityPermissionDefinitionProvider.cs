using PWD.Identity.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PWD.Identity.Permissions
{
    //public class IdentityPermissionDefinitionProvider : PermissionDefinitionProvider
    //{
    //    public override void Define(IPermissionDefinitionContext context)
    //    {
    //        var myGroup = context.AddGroup(IdentityPermissions.GroupName);
    //        //Define your own permissions here. Example:
    //        //myGroup.AddPermission(IdentityPermissions.MyPermission1, L("Permission:MyPermission1"));

    //        // Project Permission Definition Provider
    //        var projectGroup = context.AddGroup(ProjectPermissions.GroupName, L("Permission:Project"));
    //        var projectsPermission = projectGroup.AddPermission(ProjectPermissions.Projects.Default, L("Permission:Projects"));
    //        projectsPermission.AddChild(ProjectPermissions.Projects.Create, L("Permission:Projects.Create"));
    //        projectsPermission.AddChild(ProjectPermissions.Projects.Edit, L("Permission:Projects.Edit"));
    //        projectsPermission.AddChild(ProjectPermissions.Projects.Delete, L("Permission:Projects.Delete"));

    //    }

    //    private static LocalizableString L(string name)
    //    {
    //        return LocalizableString.Create<IdentityResource>(name);
    //    }
    //}

}



