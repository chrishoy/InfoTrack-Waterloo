using Waterloo.Domain.Search.Entities;

namespace Waterloo.Application.Abstractions.Services;

/// <summary>
/// Domain service to be used to scrape search results from search engine.
/// </summary>
public interface IScrapeService
{
    Task<ScrapeResults> ScrapeSearchEngine(string targetUrl, string keywords, CancellationToken ct);
}
