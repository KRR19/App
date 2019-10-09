using App.BussinesLogicLayer.Models.PrintingEdition;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.EFRepository;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Services
{
   public class PrintingEditionService : IPrintingEditionService
    {
        private readonly ApplicationContext _context;

        public PrintingEditionService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<BaseResponseModel> Create(PrintingEditionModel newPrintingEdition)
        {
            BaseResponseModel report = Validation(newPrintingEdition);
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);

            if (string.IsNullOrEmpty(report.Message))
            {
                PrintingEdition printingEdition = new PrintingEdition
                {
                    Name = newPrintingEdition.Name,
                    Description = newPrintingEdition.Description,
                    Price = newPrintingEdition.Price,
                    Status = newPrintingEdition.Status,
                    Type = newPrintingEdition.Type,
                    Currency = newPrintingEdition.Currency,
                    IsRemoved = false,
                    CreationData = DateTime.Now
                };


                report.Message = await printingEditionsRepository.Create(printingEdition);
            }

            return report;
        }

        public async Task<BaseResponseModel> Delete(Guid id)
        {
            BaseResponseModel report = new BaseResponseModel();
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);
            PrintingEdition printingEdition = await _context.PrintingEditions.FindAsync(id);

            if (printingEdition == null)
            {
                report.Message = $"Printing Edition not found in the database!";
                return report;
            }
            printingEdition.IsRemoved = true;
            report.Message = await printingEditionsRepository.Delete(printingEdition);
            return report;
        }

        public Task<PrintingEditionModel> Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public BaseResponseModel Update(PrintingEditionModel UpdateAuthor)
        {
            throw new NotImplementedException();
        }

        private BaseResponseModel Validation(PrintingEditionModel printingEdition)
        {
            return new BaseResponseModel();
        }
    }
}
