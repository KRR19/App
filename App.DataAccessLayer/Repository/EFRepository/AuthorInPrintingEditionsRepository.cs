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

        public List<Guid> GetAuthors(Guid id)
        {
            List<AuthorInPrintingEdition> authorInPrintingEditions = _context.AuthorInPrintingEditions.Where(x => x.PrintingEditionId == id).ToList();

            return authorInPrintingEditions.Select(p => p.AuthorId).ToList();
        }

        public List<string> GetAuthorsName(List<Guid> authorId)
        {
            List<string> authorsName = _context.Authors.Where(w => authorId.Contains(w.Id)).Select(s => s.Name).ToList();

            return authorsName;
        }

        public List<AuthorInPrintingEdition> GetById(Guid id)
        {
            List<AuthorInPrintingEdition> authorInPrintingEditions = _context.AuthorInPrintingEditions.Where(w => w.PrintingEditionId == id).ToList();

            return authorInPrintingEditions;
        }

        public async Task<AuthorInPrintingEdition> CreateAsync(AuthorInPrintingEdition item)
        {
            await _context.AuthorInPrintingEditions.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<AuthorInPrintingEdition> UpdateAsync(AuthorInPrintingEdition item)
        {
            _context.AuthorInPrintingEditions.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
