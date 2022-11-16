using Application.Interfaces.Contexts;
using Application.Interfaces.Repositories.Generic;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Generic
{
    public class RepositoryAsync<T, TId> : IRepositoryAsync<T, TId> where T : BaseEntity<TId>// : IEntity<TId>
    {
        #region >>> Properties <<<
        private readonly IApplicationDbContext _dbContext;
        #endregion

        #region >>> Constructor <<<

        public RepositoryAsync(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region >>> IRepositoryAsync Implementation <<<

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public async void AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        }
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext
                            .Set<T>()
                            .ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken)
        {
            return await _dbContext
                            .Set<T>()
                            .FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression, bool AsTracking, CancellationToken cancellationToken)
        {
            var baseData = _dbContext
                            .Set<T>();
            if (!AsTracking)
            {
                return await baseData
                                .AsNoTracking()
                                .FirstOrDefaultAsync(expression, cancellationToken);
            }
            return await baseData
                            .FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<List<T>> FindListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            return await _dbContext
                            .Set<T>()
                            .Where(expression)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);
        }

        public async Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _dbContext
                            .Set<T>()
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public virtual void Attach(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
        }



        #endregion

    }
}
