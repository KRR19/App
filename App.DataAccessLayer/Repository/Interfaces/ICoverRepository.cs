using App.DataAccessLayer.Entities;
using System;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface ICoverRepository
    {
        public Cover GetById(Guid id);
    }
}
