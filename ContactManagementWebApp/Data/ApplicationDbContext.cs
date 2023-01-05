using ContactManagementWebApp.Extensions;
using ContactManagementWebApp.Models.Audit;
using ContactManagementWebApp.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ContactManagementWebApp.Models.Contact;

namespace ContactManagementWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //TODO: Check if can filter all by default
            builder.FilterSoftDeletedEntries<ContactEntity>();
        }

        public override int SaveChanges()
        {
            var user = _currentUserService.GetCurrentUser().Result;
            
            this.EnsureAudit<string?>(user?.Id);
            OnBeforeSaveChanges(user?.Id ?? "NotLogged");
            
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = _currentUserService.GetCurrentUser().Result;
            
            this.EnsureAudit<string?>(user?.Id);
            OnBeforeSaveChanges(user?.Id ?? "NotLogged");
            
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }
    }
}