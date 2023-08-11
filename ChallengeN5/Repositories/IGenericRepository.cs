using ChallengeN5.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChallengeN5.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> ListAllAsync();
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(int id, T entity, CancellationToken cancellationToken);
    }
}
