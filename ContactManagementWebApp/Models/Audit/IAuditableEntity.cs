namespace ContactManagementWebApp.Models.Audit
{
    public interface IAuditableEntity<TUser>
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
        TUser CreatedBy { get; set; }
        TUser UpdatedBy { get; set; }
        TUser DeletedBy { get; set; }
    }

    public interface IAuditableEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
