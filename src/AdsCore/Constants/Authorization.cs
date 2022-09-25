namespace Ads.Core.Constants;

public static class Authorization
{
    // TODO: Don't use this in production
    public const string DEFAULT_PASSWORD = "M@j123";

    // TODO: Change this to an environment variable
    public const string JWT_SECRET_KEY = "SecretKeyOfDoomThatMustBeAMinimumNumberOfBytes";

    public static class Roles
    {
        public const string ADMINISTRATORS = "Administrators";
    }
}