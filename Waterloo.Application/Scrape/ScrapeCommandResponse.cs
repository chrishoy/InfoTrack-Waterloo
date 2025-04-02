namespace Waterloo.Application.Scrape;

public sealed class ScrapeCommandResponse
{
    public string TargetUrl { get; set; } = string.Empty;
    public int[] Positions { get; set; } = Array.Empty<int>();
}
