namespace ContactManagementWebApp.Models.Audit
{
    public interface IAuditable<TUser>
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
        TUser CreatedBy { get; set; }
        TUser UpdatedBy { get; set; }
    }

    public interface IAuditable
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
