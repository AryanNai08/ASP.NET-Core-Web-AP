using CollegeApi.Data;
using System.Linq.Expressions;

namespace CollegeApi.Repository
{
    public interface ICollegeRepository<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNotracking = false);

        //Task<T> GetByNameAsync(Expression<Func<T, bool>> filter);

        Task<T> CreateAsync(T dbrecord);

        Task<T> UpdateAsync(T dbrecord);

        Task<bool> DeleteAsync(T dbrecord);

    }
}
