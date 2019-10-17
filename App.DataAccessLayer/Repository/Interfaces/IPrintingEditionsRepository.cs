using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.Interfaces
{
    public interface IPrintingEditionsRepository : IBaseRepository<PrintingEdition>
    {
        public Task<string> Delete(PrintingEdition item);
    }
}
