namespace PWD.Identity
{
    public static class IdentityUtility
    {
        public static readonly string IDENTITY = "Identity";
        public static readonly string SCHEDULE = "Schedule";
        public static readonly string HR = "HR";
        public static readonly string PMMS = "PMMS";
        public static readonly string CMS = "CMS";
        public static readonly string Dengue = "Dengue";
        public static readonly string MySqlHR = "MySqlHR";
        public static readonly string Template = "Template";


        public static string[] ClientNames = new[]
        {
            "Identity",
            "Schedule",
            "HR",
            "PMMS",
            "CMS",
            "Dengue",
            "MySqlHR",
            "Template"
        };

        public static string[] ApiScopes = new[]
        {
            "email",
            "openid",
            "profile",
            "role",
            "phone",
            "address"
        };

        public static string[] ApiUserClaims = new[]
        {
            "email",
            "email_verified",
            "name",
            "phone_number",
            "phone_number_verified",
            "role"
        };
    }
}
