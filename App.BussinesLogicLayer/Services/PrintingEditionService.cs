using App.BussinesLogicLayer.Models.PrintingEdition;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Entities.Enum;
using App.DataAccessLayer.Repository.EFRepository;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace App.BussinesLogicLayer.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly ApplicationContext _context;
        private readonly IAuthorInPrintingEditionsRepository _authorInPrintingEditionsRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPrintingEditionsRepository _printingEditionsRepository;

        private readonly string _publicationAddedMsg = "You have successfully added a publication";
        private readonly string _publicationNotFoundMsg = "Printing Edition not found in the database!";
        private readonly string _sendNullMsg = "You send NULL!";
        private readonly string _titleOfPublicationMsg = "Enter title of publication!";
        private readonly string _negativePriceMsg = "Price cannot be negative!";
        private readonly string _StatusMsg = "Enter status of publication!";
        private readonly string _currencyMsg = "Enter currency!";
        private readonly string _typeMsg = "Enter type of publication!";

        public PrintingEditionService(ApplicationContext context, IAuthorInPrintingEditionsRepository authorInPrintingEditionsRepository, IAuthorRepository authorRepository, IPrintingEditionsRepository printingEditionsRepository)
        {
            _context = context;
            _authorInPrintingEditionsRepository = authorInPrintingEditionsRepository;
            _authorRepository = authorRepository;
            _printingEditionsRepository = printingEditionsRepository;
        }
        public async Task<BaseResponseModel> Create(PrintingEditionModel newPrintingEdition)
        {
            BaseResponseModel report = ValidationPrintingEdition(newPrintingEdition);
            PrintingEdition printingEdition = new PrintingEdition();

            if (!report.IsValid)
            {
                return report;
            }

            printingEdition.Name = newPrintingEdition.Name;
            printingEdition.Description = newPrintingEdition.Description;
            printingEdition.Price = newPrintingEdition.Price;
            printingEdition.Status = newPrintingEdition.Status;
            printingEdition.Type = newPrintingEdition.Type;
            printingEdition.Currency = newPrintingEdition.Currency;
            printingEdition.IsRemoved = false;
            printingEdition.CreationDate = DateTime.Now;

            var authorInPrintingEditions = new List<AuthorInPrintingEdition>();
            foreach (Guid authorId in newPrintingEdition.AuthorId)
            {
                var authorInPrintingEdition = new AuthorInPrintingEdition();
                authorInPrintingEdition.AuthorId = authorId;

                authorInPrintingEditions.Add(authorInPrintingEdition);
            }

            printingEdition.AuthorInPrintingEditions = authorInPrintingEditions;

            PrintingEdition addedPrintingEdition = await _printingEditionsRepository.Create(printingEdition);

           

            report.Message.Add(_publicationAddedMsg);
            return report;
        }
        public async Task<BaseResponseModel> Delete(Guid id)
        {
            BaseResponseModel report = new BaseResponseModel();
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);
            PrintingEdition printingEdition = await _context.PrintingEditions.FindAsync(id);

            if (printingEdition == null)
            {
                report.Message.Add(_publicationNotFoundMsg);
                return report;
            }
            printingEdition.IsRemoved = true;
            report.Message.Add(await printingEditionsRepository.Delete(printingEdition));

            return report;
        }
        public List<PrintingEdition> GetAll()
        {
            List<PrintingEdition> printingEdition = _printingEditionsRepository.GetAll();
            
            return printingEdition;
        }

        public async Task<PrintingEditionModel> GetById(Guid id)
        {
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);
            PrintingEdition printingEdition = await printingEditionsRepository.GetById(id);

            PrintingEditionModel printingEditionModel = new PrintingEditionModel
            {
                Id = printingEdition.Id,
                Name = printingEdition.Name,
                Description = printingEdition.Description,
                Type = printingEdition.Type,
                Currency = printingEdition.Currency,
                Price = printingEdition.Price,
                Status = printingEdition.Status,
                AuthorId = _authorInPrintingEditionsRepository.GetAuthors(printingEdition.Id),
              
            };
            printingEditionModel.AuthorName = _authorInPrintingEditionsRepository.GetAuthorsName(printingEditionModel.AuthorId);

            return printingEditionModel;
        }

        public BaseResponseModel Update(PrintingEditionModel UpdatePrintingEdition)
        {
            BaseResponseModel report = ValidationPrintingEdition(UpdatePrintingEdition);
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);

            if (!report.IsValid)
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

            printingEditionsRepository.Update(printingEdition);
            return report;
        }

        private BaseResponseModel ValidationPrintingEdition(PrintingEditionModel printingEdition)
        {
            BaseResponseModel report = new BaseResponseModel();
            if (printingEdition == null)
            {
                report.Message.Add(_sendNullMsg);
            }

            if (string.IsNullOrEmpty(printingEdition.Name) || string.IsNullOrWhiteSpace(printingEdition.Name))
            {
                report.Message.Add(_titleOfPublicationMsg);
            }
            if (printingEdition.Price < 0)
            {
                report.Message.Add(_negativePriceMsg);
            }
            if (!Enum.IsDefined(typeof(Status), printingEdition.Status))
            {
                report.Message.Add(_StatusMsg);
            }

            if (!Enum.IsDefined(typeof(Currency), printingEdition.Currency))
            {
                report.Message.Add(_currencyMsg);
            }

            if (!Enum.IsDefined(typeof(Types), printingEdition.Type))
            {
                report.Message.Add(_typeMsg);
            }

            return report;
        }
    }
}
