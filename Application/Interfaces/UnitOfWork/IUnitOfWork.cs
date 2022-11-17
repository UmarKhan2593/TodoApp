using Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.UnitOfWork
{

    public interface IUnitOfWork<TId> : IDisposable
    {
        //IRepositoryAsync<T, TId> Repository<T>() where T : AuditableEntity<TId>;
        #region >>> repositories <<<

        ITodoItemRepositoryAsync TodoItemRepository { get; }

        #endregion

        Task<int> Commit(CancellationToken cancellationToken);
        Task Rollback();
    }
}
