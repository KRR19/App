using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(User item)
        {

            bool result;

            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();
            result = true;

            return result;

        }

        public async Task<bool> Delete(User item)
        {
            bool result;
            _context.Users.Remove(item);
            await _context.SaveChangesAsync();
            result = true;
            return result;
        }

        public async Task<User> Read(Guid Id)
        {
            User user = await _context.Users.FindAsync(Id.ToString());
            return user;
        }

        public bool Update(User item)
        {
            bool result;
            _context.Users.Update(item);
            _context.SaveChanges();

            result = true;
            return result;
        }
    }
}
