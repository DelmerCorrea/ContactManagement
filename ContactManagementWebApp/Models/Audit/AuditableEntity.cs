using System.Security.Cryptography;

namespace ContactManagementWebApp.Models.Audit
{
    public abstract class AuditableEntity<TId,TUser> : EntityBase<TId>, IAuditable<TUser>
    {
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        
        public virtual TUser CreatedBy { get; set; }
        public virtual TUser UpdatedBy { get; set; }
    }

    public abstract class AuditableEntity<TId> : EntityBase<TId>, IAuditable
    {
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
    }
}
