using ContactManagementWebApp.Extensions;
using ContactManagementWebApp.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //TODO: Check if can filter all by default
            //builder.FilterSoftDeletedEntries<Contact>();
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