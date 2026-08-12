namespace UserManagement.Domain.Shared;

public interface IAuditableEntity
{
    DateTime DateCreated { get; set; }
    DateTime DateUpdated { get; set; }
    DateTime? DateDeleted { get; set; }
}