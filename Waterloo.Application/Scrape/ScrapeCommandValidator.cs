using FluentValidation;

namespace Waterloo.Application.Scrape;

public class ScrapeCommandValidator : AbstractValidator<ScrapeCommand>
{
    public ScrapeCommandValidator()
    {
        RuleFor(c => c.TargetUrl).NotEmpty();
        RuleFor(c => c.Keywords).NotEmpty();
    }
}
