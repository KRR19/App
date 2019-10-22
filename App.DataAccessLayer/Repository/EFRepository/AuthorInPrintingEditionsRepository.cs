using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
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
        public async Task<bool> Create(AuthorInPrintingEdition item)
        {
            await _context.AuthorInPrintingEditions.AddAsync(item);
            await _context.SaveChangesAsync();

            bool result;
            result = true;
            return result;
        }



        public bool Update(AuthorInPrintingEdition item)
        {
            bool result;
            _context.AuthorInPrintingEditions.Update(item);
            _context.SaveChanges();
            result = true;
            return result;
        }
    }
}
