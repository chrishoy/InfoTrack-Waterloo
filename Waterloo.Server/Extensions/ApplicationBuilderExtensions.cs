namespace Waterloo.Web.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        // This is where we'd put our builder registration points
        return app;
    }
}
