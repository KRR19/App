using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class PrintingEditionsRepository : IPrintingEditionsRepository
    {
        private readonly ApplicationContext _context;
        public PrintingEditionsRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<string> Create(PrintingEdition item)
        {
            string result;
            await _context.PrintingEditions.AddAsync(item);
            await _context.SaveChangesAsync();
            result = $"You add new PrintingEdition - {item.Name}";
            return result;
        }

        public async Task<string> Delete(PrintingEdition item)
        {
            string result;
            _context.PrintingEditions.Update(item);
            await _context.SaveChangesAsync();
            result = $"You delete {item.Name}";
            return result;
        }

        public Task<PrintingEdition> Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public string Update(PrintingEdition item)
        {
            throw new NotImplementedException();
        }
    }
}
