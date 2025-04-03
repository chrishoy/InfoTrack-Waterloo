using Waterloo.Domain.Search.Entities;
using Waterloo.Application.Abstractions.Services;

namespace Waterloo.Infrastructure.Search;

internal class GenericBatchScrapeService : IBatchScrapeService
{
    private readonly IScrapeService _scrapeService;

    public GenericBatchScrapeService(IScrapeService scrapeService)
    {
        _scrapeService = scrapeService;
    }

    public async Task<IList<ScrapeResults>> BatchScrapeSearchEngine(string targetUrl, IList<string> keywords, string searchEngine = "Google", int maxResults = 100, CancellationToken ct = default)
    {
        var results = new List<ScrapeResults>();
        
        foreach (var keyword in keywords)
        {
            try
            {
                // Add delay between requests to avoid being blocked
                if (results.Count > 0)
                {
                    await Task.Delay(2000, ct); // 2 second delay between requests
                }
                
                var result = await _scrapeService.ScrapeSearchEngine(targetUrl, keyword, searchEngine, maxResults, ct);
                results.Add(result);
            }
            catch (Exception ex)
            {
                // Log the error but continue with other keywords
                // In a real implementation, you might want to use a logger here
                Console.WriteLine($"Error scraping for keyword '{keyword}' using {searchEngine}: {ex.Message}");
                results.Add(new ScrapeResults(targetUrl, Array.Empty<int>()));
            }
            
            // Check for cancellation
            if (ct.IsCancellationRequested)
            {
                break;
            }
        }
        
        return results;
    }
} 