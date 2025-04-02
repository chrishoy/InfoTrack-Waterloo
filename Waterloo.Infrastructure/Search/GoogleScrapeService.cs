using Waterloo.Domain.Search.Entities;
using Waterloo.Application.Abstractions.Services;

namespace Waterloo.Infrastructure.Services;

internal class GoogleScrapeService : IScrapeService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GoogleScrapeService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ScrapeResults> ScrapeSearchEngine(string targetUrl, string keywords, CancellationToken ct)
    {
        await Task.Delay(2000);

        // Sample return values
        return await Task.FromResult(new ScrapeResults(targetUrl, [1, 2, 3, 4, 5]));

        var client = _httpClientFactory.CreateClient("SearchEngine");

        var escapedKeywords = keywords.Split(' ').ToList().ConvertAll(Uri.EscapeDataString);
        var query = "search?num=100&q=" + Uri.EscapeDataString(string.Join('+', escapedKeywords));
        var response = await client.GetAsync(query, ct);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            // At this point we should be able to pass the content of this to a parser and extract content according to some processing strategy,
            // such as an XPath extraction service or a CSS selector service.
            // Unfortunately, Google has a lot of anti-scraping measures in place, so we can't just parse the HTML content.
            var lines = content.Split('\n');
            var lineNumbers = new List<int>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(targetUrl))
                {
                    lineNumbers.Add(i + 1);
                }
            }
            return new ScrapeResults(targetUrl, lineNumbers.ToArray());
        }

        return new ScrapeResults(targetUrl, []);
    }
}
