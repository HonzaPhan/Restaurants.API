using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Commands.Users.Handlers
{
    public class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand>
    {
        private readonly ILogger<RemoveUserRoleCommandHandler> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RemoveUserRoleCommandHandler(
            ILogger<RemoveUserRoleCommandHandler> logger,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Removing user role: {@Request}", request);

            User user = await _userManager.FindByEmailAsync(request.UserEmail)
                ?? throw new NotFoundException(nameof(User), request.UserEmail);

            IdentityRole role = await _roleManager.FindByNameAsync(request.RoleName)
                ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

            await _userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}
