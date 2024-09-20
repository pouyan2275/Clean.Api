namespace Domain.Interfaces.Entities
{
    public interface IBaseEntity<TId>
    {
        public TId Id { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
