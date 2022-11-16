using Application.Interfaces.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {


        #region >>> Constructor <<<
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #endregion

        #region >>> DBSet Collection <<<
        public virtual DbSet<TodoItem> TodoItems { get; set; }
        public virtual DbSet<UserTodoItem> UserTodoItems { get; set; }
        #endregion



        #region >>> Interface Implementation => IApplicationDbContext <<<

        public bool HasChanges => ChangeTracker.HasChanges();

        public ChangeTracker changeTracker => ChangeTracker;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
            //modelBuilder.Seed();

        }
    }
}
