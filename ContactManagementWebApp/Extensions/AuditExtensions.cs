using ContactManagementWebApp.Models.Audit;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace ContactManagementWebApp.Extensions
{
    public static class AuditExtensions
    {
        public static void EnsureAudit<TUser>(this DbContext context, TUser userId = default(TUser))
        {
            context.ChangeTracker.DetectChanges();

            var entries = context.ChangeTracker.Entries();

            var markedAsCreated = entries.Where(x => x.State == EntityState.Added);

            foreach (var item in markedAsCreated)
            {
                if (item.Entity is IAuditableEntity<TUser> entity)
                {
                    // Only update the UpdatedAt flag - only this will get sent to the Db
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = userId;
                }
                else if (item.Entity is IAuditableEntity entity1)
                {
                    entity1.CreatedAt = DateTime.UtcNow;
                }
            }

            var markedAsUpdated = entries.Where(x => x.State == EntityState.Modified);

            foreach (var item in markedAsUpdated)
            {
                if (item.Entity is IAuditableEntity<TUser> entity)
                {
                    // Only update the UpdatedAt flag - only this will get sent to the Db
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = userId;
                }
                else if (item.Entity is IAuditableEntity entity1)
                {
                    entity1.UpdatedAt = DateTime.UtcNow;
                }
            }

            var markedAsDeleted = entries.Where(x => x.State == EntityState.Deleted);

            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is IAuditableEntity<TUser> entity)
                {
                    // Only update the DeletedAt flag - only this will get sent to the Db
                    entity.DeletedAt = DateTime.UtcNow;
                    entity.DeletedBy = userId;
                }
                else if (item.Entity is IAuditableEntity entity1)
                {
                    item.State = EntityState.Unchanged;
                    entity1.DeletedAt = DateTime.UtcNow;
                }
            }
        }
    }
}