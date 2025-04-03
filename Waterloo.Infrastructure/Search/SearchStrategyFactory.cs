using Waterloo.Application.Abstractions.Services;
using Waterloo.Infrastructure.Search.Strategies;

namespace Waterloo.Infrastructure.Search;

/// <summary>
/// Factory to create search engine specific strategies
/// </summary>
internal class SearchStrategyFactory
{
    /// <summary>
    /// Get the appropriate search strategy for the given search engine
    /// </summary>
    /// <param name="searchEngine">Name of search engine (e.g. "Google", "Bing")</param>
    /// <returns>A strategy for the specified search engine</returns>
    public static ISearchEngineStrategy GetStrategy(string searchEngine)
    {
        return searchEngine.ToLowerInvariant() switch
        {
            "google" => new GoogleSearchStrategy(),
            "bing" => new BingSearchStrategy(),
            // Add more search engines as needed
            _ => throw new ArgumentException($"Unsupported search engine: {searchEngine}", nameof(searchEngine))
        };
    }
} 