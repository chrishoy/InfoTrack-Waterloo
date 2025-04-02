namespace Waterloo.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
