using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationContext _context;
        public AuthorRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Author> Create(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return author;
        }

        public async Task<bool> Delete(Author item)
        {
            bool result;
            _context.Authors.Update(item);
            await _context.SaveChangesAsync();
            result = true;

            return result;
        }

        public async Task<Author> Update(Author item)
        {
            _context.Authors.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<Author> GetById(Guid id)
        {
            Author author = await _context.Authors.FindAsync(id);
            return author;
        }

        public List<Author> GetByAll()
        {
            var author = _context.Authors.ToList();
            return author;
        }
    }
}
