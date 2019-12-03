using App.BussinesLogicLayer.Models;
using App.BussinesLogicLayer.Models.PrintingEdition;
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
        public Task<List<PrintingEditionModel>> GetAll();
        public Task<List<PrintingEditionModel>> Filter(FilterModel filterModel);
    }
}
