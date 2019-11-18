using App.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface ICoverRepository
    {
        public Task<Cover>  GetById(Guid id);
    }
}
