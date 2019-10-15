﻿using System;
using System.Threading.Tasks;
namespace App.DataAccessLayer.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {

        public Task<string> Create(T item);
        public Task<string> Delete(T item);
        public string Update(T item);
        public Task<T> Read(Guid Id);
    }
}