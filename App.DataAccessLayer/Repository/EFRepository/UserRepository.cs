using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<User> GetAll()
        {
            List<User> users = _context.Users.ToList();

            return users;
        }

        public async Task<User> ReadAsync(Guid Id)
        {
            User user = await _userManager.FindByIdAsync(Id.ToString());
            return user;
        }

        public async Task<User> CreateAsync(User item)
        {
            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<User> UpdateAsync(User item)
        {
            _context.Users.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> Delete(User item)
        {
            bool result;
            _context.Users.Remove(item);
            await _context.SaveChangesAsync();
            result = true;

            return result;
        }
    }
}
