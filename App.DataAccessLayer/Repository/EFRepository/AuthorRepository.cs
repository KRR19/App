using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System.Threading.Tasks;
using System;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationContext _context;
        public AuthorRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<string> Create(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            
            string result;
            result = $"You add new author -  {author.Name}";
            return result;
        }

        public async Task<string> Delete(Author item)
        {
            string result;
            _context.Authors.Update(item);
            await _context.SaveChangesAsync();
            result = $"You delete {item.Name}";
            return result;

        }

        public string Update(Author item)
        {
            string result = $"You update {item.Name}";
            _context.Authors.Update(item);
            _context.SaveChanges();
            return result;
        }

        public async  Task<Author> Read(Guid id)
        {
            Author author = await _context.Authors.FindAsync(id);
            return author;
        }
    }
}
