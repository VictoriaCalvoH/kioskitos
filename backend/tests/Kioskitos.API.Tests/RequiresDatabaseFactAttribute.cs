namespace Kioskitos.API.Tests;

public sealed class RequiresDatabaseFactAttribute : FactAttribute
{
    public RequiresDatabaseFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(
                Environment.GetEnvironmentVariable("ConnectionStrings__Default")))
        {
            Skip = "ConnectionStrings__Default is not configured.";
        }
    }
}