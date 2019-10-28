using System.Threading.Tasks;
namespace App.DataAccessLayer.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<T> Create(T item);
        public Task<T> Update(T item);
    }
}
