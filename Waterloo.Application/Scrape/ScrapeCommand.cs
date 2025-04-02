using Waterloo.Application.Abstractions.Messaging;

namespace Waterloo.Application.Scrape;

public sealed class ScrapeCommand : ICommand<ScrapeCommandResponse>
{
    public string TargetUrl { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
}
