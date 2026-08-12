using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UserManagement.Domain.Shared;

namespace UserManagement.Infrastructure.Persistence.Interceptors;

public sealed class AuditableEntityInterceptor(TimeProvider timeProvider) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        UpdateAuditableEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private void UpdateAuditableEntities(DbContext? dbContext)
    {
        if (dbContext == null) return;
        
        var utcNow = timeProvider.GetUtcNow().DateTime;

        var entries = dbContext.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = utcNow;
                entry.Entity.DateUpdated = utcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.DateUpdated = utcNow;
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.DateDeleted = utcNow;
            }
        }
    }
}