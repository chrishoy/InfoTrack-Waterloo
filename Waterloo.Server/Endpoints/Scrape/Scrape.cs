using MediatR;
using Waterloo.Application.Scrape;
using Waterloo.SharedKernel;
using Waterloo.Web.Extensions;
using Waterloo.Web.Infrastructure;

namespace Waterloo.Web.Endpoints.Scrape;

internal sealed class Scrape : IEndpoint
{
    public sealed class Request
    {
        public string TargetUrl { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("scrape", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new ScrapeCommand
            {
                TargetUrl = request.TargetUrl,
                Keywords = request.Keywords
            };

            Result<ScrapeCommandResponse> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}
