using System;
using System.Threading.Tasks;
using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;

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
            Cover cover = await _context.Covers.FindAsync(id);
            return cover;
        }
    }
}
