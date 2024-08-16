namespace Restaurants.Application.Commands.Identity
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}