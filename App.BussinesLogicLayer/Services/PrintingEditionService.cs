using App.BussinesLogicLayer.Models.PrintingEdition;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Entities.Enum;
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

        public async Task<PrintingEditionModel> Read(Guid id)
        {
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);
            PrintingEdition printingEdition = await printingEditionsRepository.Read(id);

            PrintingEditionModel printingEditionModel = new PrintingEditionModel
            {
                Name = printingEdition.Name,
                Description = printingEdition.Description,
                Type = printingEdition.Type,
                Currency = printingEdition.Currency,
                Price = printingEdition.Price,
                Status = printingEdition.Status
            };

            return printingEditionModel;

        }

        public BaseResponseModel Update(PrintingEditionModel UpdatePrintingEdition)
        {
            BaseResponseModel report = Validation(UpdatePrintingEdition);
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);
            if (!string.IsNullOrEmpty(report.Message))
            {
                return report;
            }

            PrintingEdition printingEdition = new PrintingEdition
            {
                Id = UpdatePrintingEdition.Id,
                Name = UpdatePrintingEdition.Name,
                Description = UpdatePrintingEdition.Description,
                Price = UpdatePrintingEdition.Price,
                Status = UpdatePrintingEdition.Status,
                Currency = UpdatePrintingEdition.Currency,
                Type = UpdatePrintingEdition.Type
            };

            report.Message = printingEditionsRepository.Update(printingEdition);
            return report;
        }

        private BaseResponseModel Validation(PrintingEditionModel printingEdition)
        {
            BaseResponseModel report = new BaseResponseModel();
            if(printingEdition == null)
            {
                report.Message = "You send NULL!";
                return report;
            }
            if(string.IsNullOrEmpty(printingEdition.Name)||string.IsNullOrWhiteSpace(printingEdition.Name))
            {
                report.Message = "Enter title of publication!";
                return report;
            }
            if(printingEdition.Price < 0)
            {
                report.Message = "Price cannot be negative!";
                return report;
            }
            if (!Enum.IsDefined(typeof(Status),printingEdition.Status))
            {
                report.Message = "Enter status of publication!";
                return report;
            }

            if (!Enum.IsDefined(typeof(Currency), printingEdition.Currency))
            {
                report.Message = "Enter currency!";
                return report;
            }

            if (!Enum.IsDefined(typeof(Types), printingEdition.Type))
            {
                report.Message = "Enter type of publication!";
                return report;
            }

            return new BaseResponseModel();
        }
    }
}
