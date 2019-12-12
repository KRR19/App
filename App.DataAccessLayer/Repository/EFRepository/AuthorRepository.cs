using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<Author> GetByIdAsync(Guid id)
        {
            Author author = await _context.Authors.FindAsync(id);

            return author;
        }

        public List<Author> GetAll()
        {
            List<Author> author = _context.Authors.Where(x => x.IsRemoved == false).ToList();

            return author;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            EntityEntry<Author> createdAuthor = await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return createdAuthor.Entity;
        }

        public async Task<Author> UpdateAsync(Author item)
        {
            _context.Authors.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> DeleteAsync(Author item)
        {
            bool result;
            _context.Authors.Update(item);
            await _context.SaveChangesAsync();
            result = true;

            return result;
        }
    }
}
