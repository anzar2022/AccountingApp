using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccountDatabase.Repositories
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        //filter
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void DetachEntity(T entity);
    }
}
