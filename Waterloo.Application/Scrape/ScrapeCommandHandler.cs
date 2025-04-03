using Waterloo.Application.Abstractions.Messaging;
using Waterloo.Application.Abstractions.Services;
using Waterloo.SharedKernel;

namespace Waterloo.Application.Scrape;

internal sealed class ScrapeCommandHandler : ICommandHandler<ScrapeCommand, ScrapeCommandResponse>
{
    private readonly IScrapeService _scrapeService;

    public ScrapeCommandHandler(IScrapeService scrapeService)
    {
        _scrapeService = scrapeService;
    }

    public async Task<Result<ScrapeCommandResponse>> Handle(ScrapeCommand request, CancellationToken cancellationToken)
    {
        // Use default values from the service (which will use configuration)
        var result = await _scrapeService.ScrapeSearchEngine(
            request.TargetUrl, 
            request.Keywords,
            ct: cancellationToken);
            
        ScrapeCommandResponse response = new ScrapeCommandResponse
        {
            TargetUrl = result.TargetUrl,
            Positions = result.Positions,
        };

        return response;
    }
}
