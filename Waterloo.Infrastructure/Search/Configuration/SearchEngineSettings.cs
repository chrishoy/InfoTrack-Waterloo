namespace Waterloo.Infrastructure.Search.Configuration;

/// <summary>
/// Settings for search engines loaded from configuration
/// </summary>
public class SearchEngineSettings
{
    /// <summary>
    /// Default search engine to use
    /// </summary>
    public string DefaultEngine { get; set; } = "Google";
    
    /// <summary>
    /// Default maximum results to return
    /// </summary>
    public int DefaultMaxResults { get; set; } = 100;
    
    /// <summary>
    /// Collection of search engine configurations
    /// </summary>
    public Dictionary<string, SearchEngineConfig> Engines { get; set; } = new();
}

/// <summary>
/// Configuration for a specific search engine
/// </summary>
public class SearchEngineConfig
{
    /// <summary>
    /// Base URL for the search engine
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;
    
    /// <summary>
    /// Maximum results this search engine supports
    /// </summary>
    public int MaxResults { get; set; } = 100;
    
    /// <summary>
    /// HTTP headers to include in requests
    /// </summary>
    public Dictionary<string, string> Headers { get; set; } = new();
} 