using Microsoft.EntityFrameworkCore;
using newsletter_form_api.Dal;
using newsletter_form_api.Dal.Entities;
using newsletter_form_api.Dal.Repositories.Interfaces;
using System.Linq.Expressions;

namespace newsletter_form_api.Dal.Repositories.Implementations
{
    public class Repository<T>(NewsletterDbContext context) : IRepository<T> where T : BaseEntity
    {
        protected readonly NewsletterDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}