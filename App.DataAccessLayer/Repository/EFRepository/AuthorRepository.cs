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
            Author newAuthor = _context.Authors.ToList().Last();

            return newAuthor;
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

        public async Task<List<Author>> GetById(List<Guid> id)
        {
            List<Author> author = new List<Author>();
            foreach (var index in id)
            {
                author.Add(await _context.Authors.FindAsync(index));
            }
            return author;
        }

        public async Task<Author> GetById(Guid id)
        {
            Author author = await _context.Authors.FindAsync(id);

            return author;
        }

        public List<Author> GetAll()
        {
            List<Author> author = _context.Authors.Where(x => x.IsRemoved == false).ToList();

            return author;
        }
    }
}
