using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Base;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class AuthorInPrintingEditions : IAuthorInPrintingEditions
    {
        private readonly ApplicationContext _context;

        public AuthorInPrintingEditions(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<string> Create(AuthorInPrintingEdition item)
        {
            await _context.AuthorInPrintingEditions.AddAsync(item);
            await _context.SaveChangesAsync();

            string result;
            result = $"You have united {item.Author} and {item.PrintingEdition}";
            return result;
        }


        public Task<AuthorInPrintingEdition> Read(Guid Id)
        {
            throw new NotImplementedException();
        }

        public string Update(AuthorInPrintingEdition item)
        {
            string result = $"You update {item.Author} and {item.PrintingEdition}";
            _context.AuthorInPrintingEditions.Update(item);
            _context.SaveChanges();
            return result;
        }
    }
}
