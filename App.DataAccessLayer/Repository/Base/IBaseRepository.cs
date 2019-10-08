using System;
using System.Collections.Generic;
using System.Text;

namespace App.DataAccessLayer.Repository.Base
{
    public interface IBaseRepository<T>  where T : class 
    {
        public T Read(int id);
        //public void Create(T item);
        public void Update(T item);
        public void Delete(int id);
    }
}
