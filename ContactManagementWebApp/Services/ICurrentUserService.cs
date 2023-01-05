using Microsoft.AspNetCore.Identity;

namespace ContactManagementWebApp.Services
{
    public interface ICurrentUserService
    {
        Task<IdentityUser> GetCurrentUser();
    }
}
