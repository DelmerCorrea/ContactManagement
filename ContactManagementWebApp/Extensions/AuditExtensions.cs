using ContactManagementWebApp.Models.Audit;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace ContactManagementWebApp.Extensions
{
    public static class AuditExtensions
    {
        public static void FilterSoftDeletedEntries<TModel>(this ModelBuilder builder) where TModel : SoftDeleteEntity
        {
            builder.Entity<TModel>()
                    .HasQueryFilter(p => p.DeletedAt == null);
        }

        public static void EnsureAudit<TUser>(this DbContext context, TUser userId = default(TUser))
        {
            context.ChangeTracker.DetectChanges();

            var entries = context.ChangeTracker.Entries();

            var markedAsCreated = entries.Where(x => x.State == EntityState.Added);

            foreach (var item in markedAsCreated)
            {
                if (item.Entity is IAuditable<TUser> entity)
                {
                    // Only update the UpdatedAt flag - only this will get sent to the Db
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.CreatedBy = userId;
                }
                else if (item.Entity is IAuditable entity1)
                {
                    entity1.CreatedAt = DateTime.UtcNow;
                }
            }

            var markedAsUpdated = entries.Where(x => x.State == EntityState.Modified);

            foreach (var item in markedAsUpdated)
            {
                if (item.Entity is IAuditable<TUser> entity)
                {
                    // Only update the UpdatedAt flag - only this will get sent to the Db
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = userId;
                }
                else if (item.Entity is IAuditable entity1)
                {
                    entity1.UpdatedAt = DateTime.UtcNow;
                }
            }

            var markedAsDeleted = entries.Where(x => x.State == EntityState.Deleted);

            foreach (var item in markedAsDeleted)
            {
                if (item.Entity is ISoftDelete<TUser> entity)
                {
                    // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
                    item.State = EntityState.Unchanged;

                    // Only update the DeletedAt flag - only this will get sent to the Db
                    entity.DeletedAt = DateTime.UtcNow;
                    entity.DeletedBy = userId;
                }
                else if (item.Entity is ISoftDelete entity1)
                {
                    // Set the entity to unchanged (if we mark the whole entity as Modified, every field gets sent to Db as an update)
                    item.State = EntityState.Unchanged;

                    // Only update the DeletedAt flag - only this will get sent to the Db
                    entity1.DeletedAt = DateTime.UtcNow;
                }
            }
        }
    }
}