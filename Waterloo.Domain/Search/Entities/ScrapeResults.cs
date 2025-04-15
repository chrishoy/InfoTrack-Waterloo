using Waterloo.SharedKernel;

namespace Waterloo.Domain.Search.Entities;

/// <summary>
/// Represents where in the search results a Target URL appears.
/// </summary>
public sealed class ScrapeResults : Entity
{
    public string TargetUrl { get; private set; }
    public int[] Positions { get; private set; }

    public ScrapeResults(string targetUrl, int[] positions)
    {
        TargetUrl = targetUrl;
        Positions = positions;
    }
}
