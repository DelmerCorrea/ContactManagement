namespace ContactManagementWebApp.Models.Audit
{
    public abstract class SoftDeleteEntity<TUser> : SoftDeleteEntity, ISoftDelete<TUser>
    {
        public virtual TUser DeletedBy { get; set; }
    }
    public abstract class SoftDeleteEntity : ISoftDelete
    {
        public virtual DateTime? DeletedAt { get; set; }
    }
}
