using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorization
{
    public class RestaurantsUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IOptions<IdentityOptions> _optionsAccessor;

        public RestaurantsUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _optionsAccessor = optionsAccessor;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            ClaimsIdentity id = await GenerateClaimsAsync(user);

            if (user.Nationality != null && user.DateOfBirth != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
                id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
            }

            return new ClaimsPrincipal(id);
        }
    }
}
