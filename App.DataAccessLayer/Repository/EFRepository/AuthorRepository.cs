using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System.Threading.Tasks;
using System;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationContext DB;
        public AuthorRepository(ApplicationContext db)
        {
            DB = db;
        }
        public async Task<string> Create(Author author)
        {
            string result = "You add new author - ";
            await DB.Authors.AddAsync(author);
            await DB.SaveChangesAsync();
            result += author.Name;
            return result;
        }

        public async Task<string> Delete(Author item)
        {
            string result;
            DB.Authors.Update(item);
            await DB.SaveChangesAsync();
            result = $"You delete {item.Name}";
            return result;

        }

        public string Update(Author item)
        {
            string result = $"You update {item.Name}";
            DB.Authors.Update(item);
            DB.SaveChanges();
            return result;
        }

        public async  Task<Author> Read(Guid id)
        {
            Author author = await DB.Authors.FindAsync(id);
            return author;
        }
    }
}
