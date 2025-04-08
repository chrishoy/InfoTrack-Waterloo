using MediatR;
using Waterloo.Application.Scrape;
using Waterloo.SharedKernel;
using Waterloo.Web.Extensions;
using Waterloo.Web.Infrastructure;

namespace Waterloo.Web.Endpoints.Scrape;

internal sealed partial class Scrape : IEndpoint
{
    /// <summary>
    /// Maps API Endpoint to a command, executes, maps result
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("scrape", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            // Map request to command
            var command = new ScrapeCommand
            {
                TargetUrl = request.TargetUrl,
                Keywords = request.Keywords
            };

            // Execute command
            Result<ScrapeCommandResponse> result = await sender.Send(command, cancellationToken);

            // Map command result to API response
            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }
}
