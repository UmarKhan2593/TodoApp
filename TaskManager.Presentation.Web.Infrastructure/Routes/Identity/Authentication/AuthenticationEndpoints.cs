namespace TaskManager.Presentation.Web.Infrastructure.Routes.Identity.Authentication
{
    public static class AuthenticationEndpoints
    {
        public const string ResetPassword = "api/Authentication/reset-password";
        public static string ForgetPassword(string origin) => $"api/Authentication/forgot-password/?origin={origin}";
        public static string ConfirmEmail (string userId, string code) => $"api/Authentication/confirm-email/?userId={userId}&code={code}";

    }
}
