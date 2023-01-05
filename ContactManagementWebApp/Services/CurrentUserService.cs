using Microsoft.AspNetCore.Identity;

namespace ContactManagementWebApp.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceProvider _serviceProvider;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _serviceProvider = serviceProvider;
        }

        public async Task<IdentityUser> GetCurrentUser()
        {
            var userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser currentUser = null;

            if (_httpContextAccessor?.HttpContext?.User != null)
            {
                var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
                currentUser = await userManager.GetUserAsync(claimsPrincipal);
            }

            return currentUser;
        }
    }
}
