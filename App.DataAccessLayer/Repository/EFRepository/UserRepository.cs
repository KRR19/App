using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<string> Create(User item)
        {
            string result;

            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();

            result = "User was create";
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
            User user = await _userManager.FindByIdAsync(Id.ToString());
            return user;
        }

        public string Update(User item)
        {
            string result;
            _context.Users.Update(item);
            _context.SaveChanges();

            result = "User was update";
            return result;
        }
    }
}
