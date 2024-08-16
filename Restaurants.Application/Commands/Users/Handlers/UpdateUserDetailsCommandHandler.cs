using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands.Identity;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Commands.Users.Handlers
{
    public class UpdateUserDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand>
    {
        private readonly ILogger<UpdateUserDetailsCommandHandler> _logger;
        private readonly IUserContext _userContext;
        private readonly IUserStore<User> _userStore;

        public UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger, IUserContext userContext, IUserStore<User> userStore)
        {
            _logger = logger;
            _userContext = userContext;
            _userStore = userStore;
        }

        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            CurrentUser? user = _userContext.GetCurrentUser();

            _logger.LogInformation("Updating user: {UserId}, with {Request}", user!.Id, request);

            User dbUser = await _userStore.FindByIdAsync(user!.Id, cancellationToken) 
                ?? throw new NotFoundException(nameof(User), user!.Id);

            dbUser.DateOfBirth = request.DateOfBirth;
            dbUser.Nationality = request.Nationality;

            await _userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
