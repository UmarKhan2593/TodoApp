using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Generic
{
    public interface IRepositoryAsync<T, in TId> where T : class, IEntity<TId>
    {
        /// <summary>
        /// IQueryable entity of Entity Framework. Use this to execute query in database level.
        /// </summary>
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken);

        Task<T> FindAsync(Expression<Func<T, bool>> expression, bool AsTracking, CancellationToken cancellationToken);

        Task<List<T>> FindListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);

        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

        Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

        Task<T> AddAsync(T entity);

        void AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        void Attach(T entity);

    }
}
