namespace AccountDBUtilities.Repositories
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void DetachEntity(T entity);
    }
}
