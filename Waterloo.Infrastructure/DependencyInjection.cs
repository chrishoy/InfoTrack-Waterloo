using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Waterloo.Application.Abstractions.Services;
using Waterloo.Infrastructure.Search;

namespace Waterloo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register HttpClients for each supported search engine
        ConfigureSearchEngineHttpClients(services, configuration);

        // Add infrastructure services here
        services.AddScoped<IScrapeService, GenericScrapeService>();
        services.AddScoped<IBatchScrapeService, GenericBatchScrapeService>();
        return services;
    }
    
    private static void ConfigureSearchEngineHttpClients(IServiceCollection services, IConfiguration configuration)
    {
        // Define default search engines
        var searchEngines = new Dictionary<string, string>
        {
            ["Google"] = configuration["SearchUrls:Google"] ?? "https://www.google.com/",
            ["Bing"] = configuration["SearchUrls:Bing"] ?? "https://www.bing.com/"
        };
        
        // Register an HttpClient for each search engine
        foreach (var searchEngine in searchEngines)
        {
            services.AddHttpClient(searchEngine.Key, client =>
            {
                client.BaseAddress = new Uri(searchEngine.Value);
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            });
        }
    }
}
