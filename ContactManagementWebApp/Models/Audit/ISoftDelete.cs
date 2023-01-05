namespace ContactManagementWebApp.Models.Audit
{
    public interface ISoftDelete<TUser>
    {
        DateTime? DeletedAt { get; set; }
        TUser DeletedBy { get; set; }
    }

    public interface ISoftDelete
    {
        DateTime? DeletedAt { get; set; }
    }
}
