using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IPrintingEditionsRepository : IBaseRepository<PrintingEdition>
    {
        public Task<PrintingEdition> GetByIdAsync(Guid id);
        public List<PrintingEdition> GetAll();
        public Task<string> DeleteAsync(PrintingEdition item);
    }
}
