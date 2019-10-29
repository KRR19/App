﻿using App.BussinesLogicLayer.Models.PrintingEdition;
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
            AuthorInPrintingEdition authorInPrintingEdition = new AuthorInPrintingEdition();
            PrintingEdition printingEdition = new PrintingEdition();

            if (report.IsValid)
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

            await _printingEditionsRepository.Create(printingEdition);

            authorInPrintingEdition.PrintingEditionId = printingEdition.Id;
            authorInPrintingEdition.PrintingEdition = printingEdition;
            authorInPrintingEdition.Author = await _authorRepository.GetById(newPrintingEdition.Author);

            await _authorInPrintingEditionsRepository.Create(authorInPrintingEdition);

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

        public async Task<PrintingEditionModel> GetById(Guid id)
        {
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);
            PrintingEdition printingEdition = await printingEditionsRepository.GetById(id);

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
            BaseResponseModel report = ValidationPrintingEdition(UpdatePrintingEdition);
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);

            if (report.IsValid)
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
                return report;
            }


            if (string.IsNullOrEmpty(printingEdition.Name) || string.IsNullOrWhiteSpace(printingEdition.Name))
            {
                report.Message.Add(_titleOfPublicationMsg);
                return report;
            }
            if (printingEdition.Price < 0)
            {
                report.Message.Add(_negativePriceMsg);
                return report;
            }
            if (!Enum.IsDefined(typeof(Status), printingEdition.Status))
            {
                report.Message.Add(_StatusMsg);
                return report;
            }

            if (!Enum.IsDefined(typeof(Currency), printingEdition.Currency))
            {
                report.Message.Add(_currencyMsg);
                return report;
            }

            if (!Enum.IsDefined(typeof(Types), printingEdition.Type))
            {
                report.Message.Add(_typeMsg);
                return report;
            }

            return new BaseResponseModel();
        }
    }
}
