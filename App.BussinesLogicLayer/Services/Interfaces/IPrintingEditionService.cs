using App.BussinesLogicLayer.Models.PrintingEdition;
using App.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public Task<BaseResponseModel> Create(PrintingEditionModel newPrintingEdition);
        public Task<BaseResponseModel> Delete(Guid id);
        public Task<BaseResponseModel> Update(PrintingEditionModel UpdatePrintingEdition);
        public Task<PrintingEditionModel> GetById(Guid id);
        public List<PrintingEdition> GetAll();
    }
}
