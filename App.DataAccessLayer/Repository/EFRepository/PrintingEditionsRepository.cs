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

        public async Task<PrintingEdition> Read(Guid id)
        {
            PrintingEdition printingEdition = await _context.PrintingEditions.FindAsync(id);
            return printingEdition;
        }

        public string Update(PrintingEdition item)
        {
            string result = $"You update {item.Name}";
            _context.PrintingEditions.Update(item);
            _context.SaveChanges();
            return result;
        }
    }
}
