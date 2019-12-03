using App.BussinesLogicLayer.Models.PrintingEdition;
using App.BussinesLogicLayer.Services.Interfaces;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Entities.Enum;
using App.DataAccessLayer.Repository.EFRepository;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using App.BussinesLogicLayer.Models;

namespace App.BussinesLogicLayer.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly ApplicationContext _context;
        private readonly IAuthorInPrintingEditionsRepository _authorInPrintingEditionsRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IPrintingEditionsRepository _printingEditionsRepository;
        private readonly ICoverRepository _coverRepository;

        private readonly string _publicationAddedMsg = "You have successfully added a publication";
        private readonly string _publicationNotFoundMsg = "Printing Edition not found in the database!";
        private readonly string _sendNullMsg = "You send NULL!";
        private readonly string _titleOfPublicationMsg = "Enter title of publication!";
        private readonly string _negativePriceMsg = "Price cannot be negative!";
        private readonly string _StatusMsg = "Enter status of publication!";
        private readonly string _currencyMsg = "Enter currency!";
        private readonly string _typeMsg = "Enter type of publication!";

        public PrintingEditionService(ApplicationContext context, IAuthorInPrintingEditionsRepository authorInPrintingEditionsRepository, IAuthorRepository authorRepository, IPrintingEditionsRepository printingEditionsRepository, ICoverRepository coverRepository)
        {
            _context = context;
            _authorInPrintingEditionsRepository = authorInPrintingEditionsRepository;
            _authorRepository = authorRepository;
            _printingEditionsRepository = printingEditionsRepository;
            _coverRepository = coverRepository;
        }

        public async Task<List<PrintingEditionModel>> GetAll()
        {
            List<PrintingEdition> printingEditions = _printingEditionsRepository.GetAll();
            List<PrintingEditionModel> printingEditionModels = new List<PrintingEditionModel>();
           foreach (var printingEdition in printingEditions)
            {
                PrintingEditionModel printingEditionModel = new PrintingEditionModel();
                printingEditionModel.Id = printingEdition.Id;
                printingEditionModel.Name = printingEdition.Name;
                printingEditionModel.Price = printingEdition.Price;
                printingEditionModel.Description = printingEdition.Description;
                printingEditionModel.Currency = printingEdition.Currency;
                printingEditionModel.Status = printingEdition.Status;
                printingEditionModel.Type = printingEdition.Type;
                printingEditionModel.AuthorId = printingEdition.AuthorInPrintingEditions.Select(s => s.AuthorId).ToList();
                printingEditionModel.AuthorName = new List<string>();
                foreach (var authorId in printingEditionModel.AuthorId)
                {                    
                    Author author = await _authorRepository.GetById(authorId);
                    string s = author.Name;
                    printingEditionModel.AuthorName.Add(s);
                }
                printingEditionModels.Add(printingEditionModel);
            }

            return printingEditionModels;
        }

        public async Task<PrintingEditionModel> GetById(Guid id)
        {
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);
            PrintingEdition printingEdition = await printingEditionsRepository.GetById(id);
            Cover cover = _coverRepository.GetById(printingEdition.Id);

            PrintingEditionModel printingEditionModel = new PrintingEditionModel();
            printingEditionModel.Id = printingEdition.Id;
            printingEditionModel.Name = printingEdition.Name;
            printingEditionModel.Description = printingEdition.Description;
            printingEditionModel.Type = printingEdition.Type;
            printingEditionModel.Currency = printingEdition.Currency;
            printingEditionModel.Price = printingEdition.Price;
            printingEditionModel.Status = printingEdition.Status;
            printingEditionModel.AuthorId = _authorInPrintingEditionsRepository.GetAuthors(printingEdition.Id);
            printingEditionModel.Image = cover.Base64Image;

            printingEditionModel.AuthorName = _authorInPrintingEditionsRepository.GetAuthorsName(printingEditionModel.AuthorId);

            return printingEditionModel;
        }

        public async Task<List<PrintingEditionModel>> Filter(FilterModel filterModel)
        {
            List<PrintingEditionModel> printingEditionModels = await GetAll();
            List<PrintingEditionModel> FiltredPrintingEdition = new List<PrintingEditionModel>();
            printingEditionModels = printingEditionModels.Where(w => ((w.Price <= filterModel.maxPrice) && (w.Price >= filterModel.minPrice))).ToList();
            FiltredPrintingEdition = printingEditionModels;

            if (filterModel.AuthorId.Count > 0)
            {
                FiltredPrintingEdition = printingEditionModels.Where(x => x.AuthorId.Intersect(filterModel.AuthorId).Any()).ToList();
            }

            return FiltredPrintingEdition;
        }

        public async Task<BaseResponseModel> Create(PrintingEditionModel newPrintingEdition)
        {
            BaseResponseModel report = ValidationPrintingEdition(newPrintingEdition);
            PrintingEdition printingEdition = new PrintingEdition();
            Cover cover = new Cover();

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

            cover.CreationDate = DateTime.Now;
            cover.Base64Image = newPrintingEdition.Image;
            cover.PrintingEdition = printingEdition;
            printingEdition.Cover = cover;

            var authorInPrintingEditions = new List<AuthorInPrintingEdition>();
            foreach (Guid authorId in newPrintingEdition.AuthorId)
            {
                var authorInPrintingEdition = new AuthorInPrintingEdition();
                authorInPrintingEdition.AuthorId = authorId;
                authorInPrintingEditions.Add(authorInPrintingEdition);
            }

            printingEdition.AuthorInPrintingEditions = authorInPrintingEditions;

            await _printingEditionsRepository.Create(printingEdition);

            report.Message.Add(_publicationAddedMsg);
            return report;
        }

        public async Task<BaseResponseModel> Update(PrintingEditionModel UpdatePrintingEdition)
        {
            BaseResponseModel report = ValidationPrintingEdition(UpdatePrintingEdition);
            IPrintingEditionsRepository printingEditionsRepository = new PrintingEditionsRepository(_context);
            PrintingEdition printingEdition = await printingEditionsRepository.GetById(UpdatePrintingEdition.Id);
            printingEdition.AuthorInPrintingEditions = _authorInPrintingEditionsRepository.GetById(UpdatePrintingEdition.Id);
            printingEdition.Cover = _coverRepository.GetById(UpdatePrintingEdition.Id);

            if (!report.IsValid)
            {
                return report;
            }

            printingEdition.Id = UpdatePrintingEdition.Id;
            printingEdition.Name = UpdatePrintingEdition.Name;
            printingEdition.Description = UpdatePrintingEdition.Description;
            printingEdition.Price = UpdatePrintingEdition.Price;
            printingEdition.Status = UpdatePrintingEdition.Status;
            printingEdition.Currency = UpdatePrintingEdition.Currency;
            printingEdition.Type = UpdatePrintingEdition.Type;

            printingEdition.Cover.Base64Image = UpdatePrintingEdition.Image;
            printingEdition.Cover.PrintingEditionId = UpdatePrintingEdition.Id;
            printingEdition.Cover.PrintingEdition = printingEdition;

            List<AuthorInPrintingEdition> authorInPrintingEditions = new List<AuthorInPrintingEdition>();
            foreach (Guid authorId in UpdatePrintingEdition.AuthorId)
            {
                AuthorInPrintingEdition authorInPrintingEdition = new AuthorInPrintingEdition();
                authorInPrintingEdition.Author = await _authorRepository.GetById(authorId);

                authorInPrintingEditions.Add(authorInPrintingEdition);
            }



            printingEdition.AuthorInPrintingEditions = authorInPrintingEditions;

            await printingEditionsRepository.Update(printingEdition);
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
