using ChallengeN5.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeN5.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PermissionDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(PermissionDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task UpdateAsync(int id, T entity, CancellationToken cancellationToken)
        {
            var existingEntity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }

        public async Task<List<T>> ListAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

    }
}
