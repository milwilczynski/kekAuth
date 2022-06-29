namespace KekAuth.Application.Interfaces;

public interface IDateTimeProvider
{
    DateTimeOffset Now { get; }
}