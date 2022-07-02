namespace KekAuth.Application.Interfaces;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}