using Application.Interfaces.Contexts;
using Application.Interfaces.Repositories;
using Application.Interfaces.UnitOfWork;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork<TId> : IUnitOfWork<TId>
    {
        #region >>> Properties <<<
        private readonly IApplicationDbContext _dbContext;
        private bool disposed;

        #endregion

        #region >>> Constructor <<<

        public UnitOfWork(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        #endregion

        #region >>> IUnitOfWork Implementation <<<

        #region >>> Hold Repository object in unit of work<<< 

        private ITodoItemRepositoryAsync _papersRepository;

        public ITodoItemRepositoryAsync papersRepository
        {
            get { return _papersRepository ??= new TodoItemRepository(_dbContext); }
        }



        #endregion


        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task Rollback()
        {
            _dbContext.changeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                //dispose managed resources
                _dbContext.Dispose();
            }
            //dispose unmanaged resources
            disposed = true;
        }


        #endregion
    }
}
