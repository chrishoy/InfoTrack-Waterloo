using Waterloo.Domain.Search.Entities;

namespace Waterloo.Application.Abstractions.Services;

/// <summary>
/// Domain service to be used to scrape search results from search engine.
/// </summary>
public interface IScrapeService
{
    /// <summary>
    /// Scrapes search results from the specified search engine
    /// </summary>
    /// <param name="targetUrl">URL to search for in results</param>
    /// <param name="keywords">Search terms</param>
    /// <param name="searchEngine">The search engine to use (e.g. "Google", "Bing")</param>
    /// <param name="maxResults">Maximum number of results to process</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Positions where the target URL appears in search results</returns>
    Task<ScrapeResults> ScrapeSearchEngine(string targetUrl, string keywords, string searchEngine = "Google", int maxResults = 100, CancellationToken ct = default);
}
