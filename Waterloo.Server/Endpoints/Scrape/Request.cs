namespace Waterloo.Web.Endpoints.Scrape;

internal sealed partial class Scrape
{
    /// <summary>
    /// API Request contract for <see cref="Scrape"/> 
    /// </summary>
    public sealed class Request
    {
        public string TargetUrl { get; set; } = string.Empty;
        public string Keywords { get; set; } = string.Empty;
    }
}
