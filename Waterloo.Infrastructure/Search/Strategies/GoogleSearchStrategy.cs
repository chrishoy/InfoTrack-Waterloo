using System.Text.RegularExpressions;
using Waterloo.Application.Abstractions.Services;

namespace Waterloo.Infrastructure.Search.Strategies;

internal class GoogleSearchStrategy : ISearchEngineStrategy
{
    public string BuildSearchQuery(string keywords, int maxResults = 100)
    {
        var escapedKeywords = Uri.EscapeDataString(keywords);
        return $"search?num={maxResults}&q={escapedKeywords}";
    }
    
    public int[] ExtractPositions(string htmlContent, string targetUrl)
    {
        var positions = new List<int>();
        var resultIndex = 1;
        
        // Normalize the target URL for comparison
        var normalizedTargetUrl = NormalizeUrl(targetUrl);
        
        // Use Regex to find result entries in Google HTML
        // This pattern matches Google search result links
        var resultPattern = new Regex(@"<div class=""yuRUbf"">.*?<a href=""(.*?)"".*?</div>", 
            RegexOptions.Singleline);
            
        var matches = resultPattern.Matches(htmlContent);
        
        foreach (Match match in matches)
        {
            if (match.Success && match.Groups.Count > 1)
            {
                var extractedUrl = match.Groups[1].Value;
                var normalizedExtractedUrl = NormalizeUrl(extractedUrl);
                
                if (normalizedExtractedUrl.Contains(normalizedTargetUrl))
                {
                    positions.Add(resultIndex);
                }
                
                resultIndex++;
            }
        }
        
        // Fallback to simpler approach if no results found with regex
        if (positions.Count == 0)
        {
            var lines = htmlContent.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(targetUrl))
                {
                    // Since we don't know exact position, just record line number where URL was found
                    positions.Add(i + 1);
                }
            }
        }
        
        return positions.ToArray();
    }
    
    private string NormalizeUrl(string url)
    {
        // Remove protocol (http:// or https://)
        url = Regex.Replace(url, @"^https?://", "", RegexOptions.IgnoreCase);
        
        // Remove trailing slash if present
        url = url.TrimEnd('/');
        
        // Remove www. if present
        url = Regex.Replace(url, @"^www\.", "", RegexOptions.IgnoreCase);
        
        return url.ToLowerInvariant();
    }
} 