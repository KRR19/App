using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private ApplicationContext DB;
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

        public void Delete(int id)
        {
            Author item = DB.Authors.Find(id);
            if (item != null)
            {
                item.IsRemoved = true;
                DB.Authors.Update(item);
                DB.SaveChanges();
            }
        }

        public Author Read(int id)
        {
            return DB.Authors.Find(id);

        }

        public void Update(Author item)
        {
            if (item != null)
            {
                DB.Authors.Update(item);
                DB.SaveChanges();
            }

        }
    }
}
