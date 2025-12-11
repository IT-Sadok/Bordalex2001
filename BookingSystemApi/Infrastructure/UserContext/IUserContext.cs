namespace Infrastructure.UserContext;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}
