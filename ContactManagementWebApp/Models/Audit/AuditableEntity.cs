using System.Security.Cryptography;

namespace ContactManagementWebApp.Models.Audit
{
    public abstract class AuditableEntity<TId,TUser> : EntityBase<TId>, IAuditableEntity<TUser>
    {
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual TUser CreatedBy { get; set; }
        public virtual TUser UpdatedBy { get; set; }
        public virtual TUser DeletedBy { get; set; }
    }

    public abstract class AuditableEntity<TId> : EntityBase<TId>, IAuditableEntity
    {
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
    }
}
