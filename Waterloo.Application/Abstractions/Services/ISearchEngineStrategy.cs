using Waterloo.Domain.Search.Entities;

namespace Waterloo.Application.Abstractions.Services;

/// <summary>
/// Strategy interface for search engine specific implementations
/// </summary>
public interface ISearchEngineStrategy
{
    /// <summary>
    /// Builds a search query specific to this search engine
    /// </summary>
    string BuildSearchQuery(string keywords, int maxResults = 100);
    
    /// <summary>
    /// Extracts positions of the target URL from search results HTML
    /// </summary>
    int[] ExtractPositions(string htmlContent, string targetUrl);
} 