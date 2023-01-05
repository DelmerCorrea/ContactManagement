using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ContactManagementWebApp.Models.Audit
{
    public abstract class SoftDeleteEntity<TUser> : SoftDeleteEntity, ISoftDelete<TUser>
    {
        [ValidateNever]
        public virtual TUser? DeletedBy { get; set; }
    }
    public abstract class SoftDeleteEntity : ISoftDelete
    {
        [ValidateNever]
        public virtual DateTime? DeletedAt { get; set; }
    }
}
