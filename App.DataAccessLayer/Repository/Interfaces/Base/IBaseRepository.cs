using System.Threading.Tasks;
namespace App.DataAccessLayer.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T> CreateAsync(T item);
        public Task<T> UpdateAsync(T item);
    }
}
