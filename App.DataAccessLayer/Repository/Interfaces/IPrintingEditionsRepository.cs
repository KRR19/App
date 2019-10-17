using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IPrintingEditionsRepository : IBaseRepository<PrintingEdition>
    {
        public Task<PrintingEdition> GetById(Guid id);
        public List<PrintingEdition> GetAll();
        public Task<string> Delete(PrintingEdition item);
    }
}
