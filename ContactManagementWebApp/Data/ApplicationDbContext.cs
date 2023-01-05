using ContactManagementWebApp.Extensions;
using ContactManagementWebApp.Models.Audit;
using ContactManagementWebApp.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ContactManagementWebApp.Models.Contact;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;

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
            SeedDefaultUser(builder);

            //TODO: Check if can filter all by default
            builder.FilterSoftDeletedEntries<ContactEntity>();

            base.OnModelCreating(builder);
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

        private void SeedDefaultUser(ModelBuilder builder)
        {
            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

            //seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            //create user
            var appUser = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "alfasoft@alfasoft.com",
                EmailConfirmed = true,
                UserName = "alfasoft@alfasoft.com",
                NormalizedUserName = "ALFASOFT@ALFASOFT.COM"
            };

            //set user password
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "alfasoft");

            //seed user
            builder.Entity<IdentityUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}