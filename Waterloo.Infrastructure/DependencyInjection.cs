using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Waterloo.Application.Abstractions.Services;
using Waterloo.Infrastructure.Search;
using Waterloo.Infrastructure.Search.Configuration;

namespace Waterloo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Load search engine settings from config
        var searchEngineSettings = new SearchEngineSettings();
        configuration.GetSection("SearchEngine").Bind(searchEngineSettings);
        
        // Add default configurations if none provided
        EnsureDefaultSearchEngines(searchEngineSettings);
        
        // Register settings as singleton
        services.AddSingleton(searchEngineSettings);
        
        // Register HttpClients for each supported search engine
        ConfigureSearchEngineHttpClients(services, searchEngineSettings);

        // Add infrastructure services here
        services.AddScoped<IScrapeService, GenericScrapeService>();
        services.AddScoped<IBatchScrapeService, GenericBatchScrapeService>();
        return services;
    }
    
    private static void EnsureDefaultSearchEngines(SearchEngineSettings settings)
    {
        // Ensure Google is available
        if (!settings.Engines.ContainsKey("Google"))
        {
            settings.Engines["Google"] = new SearchEngineConfig
            {
                BaseUrl = "https://www.google.com/",
                MaxResults = 100,
                Headers = new Dictionary<string, string>
                {
                    ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
                    ["Accept-Language"] = "en-US,en;q=0.9"
                }
            };
        }
        
        // Ensure Bing is available
        if (!settings.Engines.ContainsKey("Bing"))
        {
            settings.Engines["Bing"] = new SearchEngineConfig
            {
                BaseUrl = "https://www.bing.com/",
                MaxResults = 100,
                Headers = new Dictionary<string, string>
                {
                    ["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36",
                    ["Accept-Language"] = "en-US,en;q=0.9"
                }
            };
        }
    }
    
    private static void ConfigureSearchEngineHttpClients(IServiceCollection services, SearchEngineSettings settings)
    {
        // Register an HttpClient for each search engine
        foreach (var engine in settings.Engines)
        {
            services.AddHttpClient(engine.Key, client =>
            {
                client.BaseAddress = new Uri(engine.Value.BaseUrl);
                
                // Add headers from configuration
                foreach (var header in engine.Value.Headers)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            });
        }
    }
}
