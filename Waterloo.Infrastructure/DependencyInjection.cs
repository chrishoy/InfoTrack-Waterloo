using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Waterloo.Application.Abstractions.Services;
using Waterloo.Infrastructure.Services;

namespace Waterloo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var searchUrl = configuration["SearchUrl"]!; // Fetch search endpoint (i.e. Google) from appsettings.json

        // Add HttpClient which will be used to perform search
        services.AddHttpClient("SearchEngine", client =>
        {
            client.BaseAddress = new Uri(searchUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            //client.DefaultRequestHeaders.Add("User-Agent", "Lynx/2.8.9rel.1 libwww-FM/2.14"); // Mimic a text-based browser [Still doesn't work with Google]
            //client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            //client.DefaultRequestHeaders.Add("Save-Data", "on"); // Request a low-bandwidth page
        });

        // Add infrastructure services here
        services.AddScoped<IScrapeService, GoogleScrapeService>();
        return services;
    }
}
