using Microsoft.Extensions.Configuration;
using Waterloo.Domain.Search.Entities;
using Waterloo.Application.Abstractions.Services;

namespace Waterloo.Infrastructure.Search;

internal class GenericScrapeService : IScrapeService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public GenericScrapeService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<ScrapeResults> ScrapeSearchEngine(string targetUrl, string keywords, string searchEngine = "Google", int maxResults = 100, CancellationToken ct = default)
    {
        // Get the appropriate strategy for the search engine
        var strategy = Search.SearchStrategyFactory.GetStrategy(searchEngine);
        
        // Get the base URL for the search engine from configuration or use a default
        var baseUrl = GetSearchEngineBaseUrl(searchEngine);
        var client = _httpClientFactory.CreateClient(searchEngine);
        client.BaseAddress = new Uri(baseUrl);
        
        // Build the search query using the strategy
        var query = strategy.BuildSearchQuery(keywords, maxResults);
        var response = await client.GetAsync(query, ct);
        
        if (!response.IsSuccessStatusCode)
        {
            return new ScrapeResults(targetUrl, Array.Empty<int>());
        }
        
        var content = await response.Content.ReadAsStringAsync(ct);
        
        // Extract positions using the strategy
        var positions = strategy.ExtractPositions(content, targetUrl);
        
        return new ScrapeResults(targetUrl, positions);
    }
    
    private string GetSearchEngineBaseUrl(string searchEngine)
    {
        // Get the search engine URL from configuration
        var configKey = $"SearchUrls:{searchEngine}";
        var url = _configuration[configKey];
        
        // If not found, use defaults
        if (string.IsNullOrEmpty(url))
        {
            url = searchEngine.ToLowerInvariant() switch
            {
                "google" => "https://www.google.com/",
                "bing" => "https://www.bing.com/",
                _ => throw new ArgumentException($"No URL configured for search engine: {searchEngine}")
            };
        }
        
        return url;
    }
} 