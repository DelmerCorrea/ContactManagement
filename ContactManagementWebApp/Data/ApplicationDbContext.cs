using ContactManagementWebApp.Extensions;
using ContactManagementWebApp.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        public override int SaveChanges()
        {
            var user = _currentUserService.GetCurrentUser().Result;
            this.EnsureAudit<string?>(user?.Id);
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = _currentUserService.GetCurrentUser().Result;
            this.EnsureAudit<string?>(user?.Id);
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}