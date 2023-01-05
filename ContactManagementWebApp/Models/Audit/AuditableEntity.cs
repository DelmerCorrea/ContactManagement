using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Security.Cryptography;

namespace ContactManagementWebApp.Models.Audit
{
    public abstract class AuditableEntity<TId,TUser> : EntityBase<TId>, IAuditable<TUser>
    {
        [ValidateNever]
        public virtual DateTime CreatedAt { get; set; }
        [ValidateNever]
        public virtual DateTime? UpdatedAt { get; set; }
        
        [ValidateNever]
        public virtual TUser CreatedBy { get; set; }
        [ValidateNever]
        public virtual TUser? UpdatedBy { get; set; }
    }

    public abstract class AuditableEntity<TId> : EntityBase<TId>, IAuditable
    {
        [ValidateNever]
        public virtual DateTime CreatedAt { get; set; }
        [ValidateNever]
        public virtual DateTime? UpdatedAt { get; set; }
    }
}
