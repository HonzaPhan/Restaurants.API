using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands.Identity;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    internal class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;
        private readonly IUserContext _userContext;

        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger, IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var currentUser = _userContext.GetCurrentUser();

            _logger.LogInformation("User: {Email}, date of birth: {DateOfBirth}", currentUser.Email, currentUser.DateOfBirth);

            if (currentUser.DateOfBirth == null)
            {
                _logger.LogWarning("Authorization denied! User: {Email} does not have a date of birth", currentUser.Email);
                context.Fail();
                return Task.CompletedTask;
            }

            DateOnly dateOfBirth = currentUser.DateOfBirth.Value;
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            if (dateOfBirth.AddYears(requirement.MinimumAge) <= today)
            {
                _logger.LogInformation("Authorization granted for user: {Email}", currentUser.Email);
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogWarning("Authorization denied! User: {Email} is not old enough", currentUser.Email);
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
