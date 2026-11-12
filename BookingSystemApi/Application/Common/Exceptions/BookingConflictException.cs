namespace Application.Common.Exceptions;

public sealed class BookingConflictException(string message) : Exception(message)
{
}
