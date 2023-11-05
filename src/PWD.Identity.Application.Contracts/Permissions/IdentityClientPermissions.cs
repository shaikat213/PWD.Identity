namespace PWD.Identity.Permissions
{
    public static class IdentityClientPermissions
    {
        public const string GroupName = "AbpClientManagement";

        public static class IdentityClients
        {
            public const string Default = GroupName + ".Clients";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }
    }
}