using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.Commands.Identity
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            ClaimsPrincipal user = httpContextAccessor?.HttpContext?.User 
                ?? throw new InvalidOperationException("User context is not available");

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            string id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            string email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            IEnumerable<string> roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            string? nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
            string? dateOfBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
            DateOnly? dateOfBirth = dateOfBirthString == null ? (DateOnly?)null : DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd");

            return new CurrentUser(id, email, roles, nationality, dateOfBirth);
        }
    }
}
