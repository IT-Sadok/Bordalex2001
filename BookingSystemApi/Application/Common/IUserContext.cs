namespace Application.Common;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}
