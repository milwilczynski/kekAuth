using KekAuth.Application.Interfaces;

namespace KekAuth.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}