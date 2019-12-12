using App.BussinesLogicLayer.Models;
using App.BussinesLogicLayer.Models.PrintingEdition;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public Task<BaseResponseModel> CreateAsync(PrintingEditionModel newPrintingEdition);
        public Task<BaseResponseModel> DeleteAsync(Guid id);
        public Task<BaseResponseModel> UpdateAsync(PrintingEditionModel UpdatePrintingEdition);
        public Task<PrintingEditionModel> GetByIdAsync(Guid id);
        public Task<List<PrintingEditionModel>> GetAllAsync();
        public Task<List<PrintingEditionModel>> FilterAsync(FilterModel filterModel);
    }
}
