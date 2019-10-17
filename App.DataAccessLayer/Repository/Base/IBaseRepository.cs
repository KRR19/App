using System;
using System.Threading.Tasks;
namespace App.DataAccessLayer.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {

        public Task<bool> Create(T item);
        public bool Update(T item);
    }
}
