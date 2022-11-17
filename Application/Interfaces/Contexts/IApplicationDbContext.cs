
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        bool HasChanges { get; }
        ChangeTracker changeTracker { get; }

        void Dispose();

        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        EntityEntry Entry(object entity);

       DbSet<TodoItem> TodoItems { get; set; }

    }
}
