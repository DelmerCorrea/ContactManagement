namespace ContactManagementWebApp.Models.Audit
{
    public interface ISoftDelete<TUser> : ISoftDelete
    {
        TUser DeletedBy { get; set; }
    }

    public interface ISoftDelete
    {
        DateTime? DeletedAt { get; set; }
    }
}
