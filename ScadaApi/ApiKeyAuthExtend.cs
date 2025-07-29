using ScadaApi;

public static class ApiKeyAuthExtend
{
    public static IApplicationBuilder UseApiKeyAuth(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ApiKeyAuth>();
    }
}
