using Microsoft.Extensions.Configuration;
using Waterloo.Domain.Search.Entities;
using Waterloo.Application.Abstractions.Services;
using Waterloo.Infrastructure.Search.Configuration;

namespace Waterloo.Infrastructure.Search;

internal class GenericScrapeService : IScrapeService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly SearchEngineSettings _searchEngineSettings;

    public GenericScrapeService(IHttpClientFactory httpClientFactory, SearchEngineSettings searchEngineSettings)
    {
        _httpClientFactory = httpClientFactory;
        _searchEngineSettings = searchEngineSettings;
    }

    public async Task<ScrapeResults> ScrapeSearchEngine(string targetUrl, string keywords, string searchEngine = "Google", int maxResults = 100, CancellationToken ct = default)
    {
        // If no search engine specified, use the default from config
        searchEngine ??= _searchEngineSettings.DefaultEngine;
        
        // Ensure the search engine is supported
        if (!_searchEngineSettings.Engines.ContainsKey(searchEngine))
        {
            throw new ArgumentException($"Unsupported search engine: {searchEngine}", nameof(searchEngine));
        }
        
        // Get the appropriate strategy for the search engine
        var strategy = SearchStrategyFactory.GetStrategy(searchEngine);
        
        // Get engine config and determine max results
        var engineConfig = _searchEngineSettings.Engines[searchEngine];
        
        // If specified maxResults exceeds the engine's limit, use the engine's limit
        maxResults = Math.Min(maxResults, engineConfig.MaxResults);
        
        // Use the HTTP client pre-configured for this search engine
        var client = _httpClientFactory.CreateClient(searchEngine);
        
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
} 