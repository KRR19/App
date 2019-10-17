﻿using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<bool> Create(PrintingEdition item)
        {
            bool result;
            await _context.PrintingEditions.AddAsync(item);
            await _context.SaveChangesAsync();
            result = true;
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

        public async Task<PrintingEdition> GetById(Guid id)
        {
            PrintingEdition printingEdition = await _context.PrintingEditions.FindAsync(id);
            return printingEdition;
        }

        public List<PrintingEdition> GetAll()
        {
            var printingEdition = _context.PrintingEditions.ToList();
            return printingEdition;
        }

        public bool Update(PrintingEdition item)
        {
            bool result;
            _context.PrintingEditions.Update(item);
            _context.SaveChanges();
            result = true;
            return result;
        }

                     
    }
}
