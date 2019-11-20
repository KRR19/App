﻿using System;
using System.Threading.Tasks;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System.Linq;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class CoverRepository : ICoverRepository
    {
        private readonly ApplicationContext _context;

        public CoverRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Cover> GetById(Guid id)
        {
            Cover cover =  _context.Covers.FirstOrDefault(p => p.PrintingEditionId == id);
            return cover;
        }
    }
}
