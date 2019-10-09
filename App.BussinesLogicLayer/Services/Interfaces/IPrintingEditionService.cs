using App.BussinesLogicLayer.Models.PrintingEdition;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        public Task<BaseResponseModel> Create(PrintingEditionModel newPrintingEdition);
        public Task<BaseResponseModel> Delete(Guid id);
        public BaseResponseModel Update(PrintingEditionModel UpdateAuthor);
        public Task<PrintingEditionModel> Read(Guid id);
    }
}
