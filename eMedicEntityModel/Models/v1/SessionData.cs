namespace eMedicEntityModel.Models.v1
{
    public static class SessionData
    {
        public static ApplicationUser? ApplicationUser { get; set; }
        public static string Username { get; set; } = string.Empty;
        public static string AuthToken { get; set; } = string.Empty;
        public static string AppName { get; set; } = string.Empty;
    }
}
