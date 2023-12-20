using Duende.IdentityServer.Services;

namespace neighbor_chef.Services.Cors;

public class MyCorsPolicyService : ICorsPolicyService
{
    private readonly ILogger<MyCorsPolicyService> _logger;

    public MyCorsPolicyService(ILogger<MyCorsPolicyService> logger)
    {
        _logger.LogInformation("Hello, CORS!");
        _logger = logger;
    }

    public Task<bool> IsOriginAllowedAsync(string origin)
    {
        _logger.LogInformation("Hello, world!");
        _logger.LogInformation($"CORS request for origin: {origin}");
        return Task.FromResult(true);
    }
}
