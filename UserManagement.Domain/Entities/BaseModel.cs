using UserManagement.Domain.Shared;

namespace UserManagement.Domain.Entities;

public abstract class BaseModel: IAuditableEntity
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    public DateTime? DateDeleted { get; set; }
}