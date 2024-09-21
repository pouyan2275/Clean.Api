using Domain.Bases.Interfaces.Entities;

namespace Domain.Entities;

public class Category : IBaseEntity<Guid>
{   
    public string? Title { get; set; }
    public Guid Id { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
}
