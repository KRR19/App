using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class AuthorInPrintingEditionsRepository : IAuthorInPrintingEditionsRepository
    {
        private readonly ApplicationContext _context;

        public AuthorInPrintingEditionsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<AuthorInPrintingEdition> Create(AuthorInPrintingEdition item)
        {
            await _context.AuthorInPrintingEditions.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public List<Guid> GetAuthors(Guid id)
        {
            List<AuthorInPrintingEdition> authorInPrintingEditions = _context.AuthorInPrintingEditions.Where(x=> x.PrintingEditionId == id).ToList();
            return authorInPrintingEditions.Select(p => p.AuthorId).ToList();
        }

        public List<string> GetAuthorsName(List<Guid> authorId)
        {
            List<Author> authorList = new List<Author>();
            List<string> authorsName = new List<string>();
            foreach(var author in authorId)
            {
                authorList.Add(_context.Authors.Where(p => p.Id == author).FirstOrDefault());
            }
            authorsName = authorList.Select(p => p.Name).ToList();
            return authorsName;
        }

        public async Task<AuthorInPrintingEdition> Update(AuthorInPrintingEdition item)
        {
            _context.AuthorInPrintingEditions.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
