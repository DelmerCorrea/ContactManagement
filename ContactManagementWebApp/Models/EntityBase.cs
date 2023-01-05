namespace ContactManagementWebApp.Models
{
    public abstract class EntityBase<TId> : IEntityBase<TId>
    {
        public virtual TId Id { get; set; }
    }
}
