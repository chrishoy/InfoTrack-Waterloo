using System.Text.RegularExpressions;
using Waterloo.Application.Abstractions.Services;

namespace Waterloo.Infrastructure.Search.Strategies;

internal class BingSearchStrategy : ISearchEngineStrategy
{
    public string BuildSearchQuery(string keywords, int maxResults = 100)
    {
        var escapedKeywords = Uri.EscapeDataString(keywords);
        return $"search?count={maxResults}&q={escapedKeywords}";
    }
    
    public int[] ExtractPositions(string htmlContent, string targetUrl)
    {
        var positions = new List<int>();
        var resultIndex = 1;
        
        // Normalize the target URL for comparison
        var normalizedTargetUrl = NormalizeUrl(targetUrl);
        
        // Pattern for Bing search results (this is an example - real pattern may differ)
        var resultPattern = new Regex(@"<li class=""b_algo"">.*?<a href=""(.*?)"".*?</li>", 
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