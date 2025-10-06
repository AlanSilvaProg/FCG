using StudyCaseWebApi.Middlewares.Auth;

namespace StudyCaseWebApi.Extensions;

public static class AuthenticationMiddlewareExtensions
{
    /// <summary>
    /// Authentication Middleware
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseAuthenticationValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}