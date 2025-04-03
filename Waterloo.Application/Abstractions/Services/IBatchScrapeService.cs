using Waterloo.Domain.Search.Entities;

namespace Waterloo.Application.Abstractions.Services;

/// <summary>
/// Domain service to be used to scrape search results for multiple keywords.
/// </summary>
public interface IBatchScrapeService
{
    /// <summary>
    /// Scrapes search engine for each keyword in the list and finds the position of the target URL
    /// </summary>
    /// <param name="targetUrl">The URL to search for in results</param>
    /// <param name="keywords">List of keywords to search for</param>
    /// <param name="searchEngine">The search engine to use (e.g. "Google", "Bing")</param>
    /// <param name="maxResults">Maximum number of results to return (default 100)</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>A list of scrape results, one for each keyword</returns>
    Task<IList<ScrapeResults>> BatchScrapeSearchEngine(string targetUrl, IList<string> keywords, string searchEngine = "Google", int maxResults = 100, CancellationToken ct = default);
} 