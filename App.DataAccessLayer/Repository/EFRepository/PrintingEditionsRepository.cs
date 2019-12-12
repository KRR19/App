using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class PrintingEditionsRepository : IPrintingEditionsRepository
    {
        private readonly ApplicationContext _context;
        public PrintingEditionsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<PrintingEdition> GetByIdAsync(Guid id)
        {
            PrintingEdition printingEdition = await _context.PrintingEditions.FindAsync(id);

            return printingEdition;
        }

        public List<PrintingEdition> GetAll()
        {
            List<PrintingEdition> printingEditions = _context.PrintingEditions.Where(p => (p.IsRemoved == false)).Include(i => i.AuthorInPrintingEditions).ToList();
            for(int i=0; i < printingEditions.Count; i++)
            {
                for(int a = 0; a < printingEditions[i].AuthorInPrintingEditions.Count; a++)
                {
                    printingEditions[i].AuthorInPrintingEditions[a].Author = _context.Authors.Where(w => w.Id == printingEditions[i].AuthorInPrintingEditions[a].AuthorId).FirstOrDefault();
                }
            }

            return printingEditions;
        }


        public async Task<PrintingEdition> CreateAsync(PrintingEdition item)
        {
            await _context.PrintingEditions.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<PrintingEdition> UpdateAsync(PrintingEdition item)
        {
            _context.PrintingEditions.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
        public async Task<string> DeleteAsync(PrintingEdition item)
        {
            string result;
            _context.PrintingEditions.Update(item);
            await _context.SaveChangesAsync();
            result = $"You delete {item.Name}";

            return result;
        }
    }
}
