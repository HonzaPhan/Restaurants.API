using MediatR;

namespace Restaurants.Application.Commands.Users
{
    public class AssignUserRoleCommand : IRequest
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
