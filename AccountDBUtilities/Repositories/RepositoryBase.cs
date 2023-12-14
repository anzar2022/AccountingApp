using AccountDBUtilities.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountDBUtilities.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        protected AccountingAppDBContext  _accountingAppDBContext { get; set; }
        //public RepositoryBase(BillingAppContext billingAppContext)
        //{
        //    _billingAppContext = billingAppContext;
        //}

        public RepositoryBase(AccountingAppDBContext accountingAppDBContext)
        {
            _accountingAppDBContext = accountingAppDBContext ?? throw new ArgumentNullException(nameof(accountingAppDBContext));
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            // return await _billingAppContext.FindAsync<T>(id);
            var entity = await _accountingAppDBContext.FindAsync<T>(id);
            if (entity != null)
            {
                _accountingAppDBContext.Entry(entity).State = EntityState.Detached;
            }
            return entity;
        }

     

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //return await _billingAppContext.Set<T>().ToListAsync();
            var entities = await _accountingAppDBContext.Set<T>().AsNoTracking().ToListAsync();

            foreach (var entity in entities)
            {
                _accountingAppDBContext.Entry(entity).State = EntityState.Detached;
            }

            return entities;
        }

        public async Task<T> CreateAsync(T entity)
        {
            _accountingAppDBContext.Add(entity);
            try
            {
                await _accountingAppDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
            }
            _accountingAppDBContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        //public async Task<T> UpdateAsync(T entity)
        //{
        //    _billingAppContext.Attach(entity);
        //    _billingAppContext.Entry(entity).State = EntityState.Modified;
        //    await _billingAppContext.SaveChangesAsync();
        //  //  _billingAppContext.Entry(entity).State = EntityState.Detached;
        //    return entity;
        //}
        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _accountingAppDBContext.Attach(entity);
                _accountingAppDBContext.Entry(entity).State = EntityState.Modified;
                await _accountingAppDBContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it or perform any other error handling.
                // You can also throw a custom exception if needed.
                throw new ApplicationException("An error occurred while updating the entity.", ex);
            }
        }


        public async Task DeleteAsync(T entity)
        {
            _accountingAppDBContext.Remove(entity);
            await _accountingAppDBContext.SaveChangesAsync();
        }
        public void DetachEntity(T entity)
        {
            _accountingAppDBContext.Entry(entity).State = EntityState.Detached;
        }
        public void Dispose()
        {
            _accountingAppDBContext.Dispose();
        }
    }
}
