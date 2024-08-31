using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;
using Ordering.Infrastructure.Extensions;

namespace Ordering.Infrastructure.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        
        private void UpdateEntities(DbContext context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<IEntity>()) 
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "sysuser";
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities()) 
                {
                    entry.Entity.LastModifiedBy = "sysuser";
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                }
            }
        }
    }
}