using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace App.DataAccessLayer.Repository.EFRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<string> Create(User item)
        {

            string result;
            
            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();
            result = $"You add new user - {item.UserName}";

            return result;

        }

        public async Task<string> Delete(User item)
        {
            string result;
            _context.Users.Remove(item);
            await _context.SaveChangesAsync();
            result = $"You delete {item.UserName}";
            return result;
        }

        public async Task<User> Read(Guid Id)
        {
            User user = await _context.Users.FindAsync(Id);
            return user;
        }

        public string Update(User item)
        {
            string result = $"You update {item.UserName}";
            _context.Users.Update(item);
            _context.SaveChanges();
            return result;
        }
    }
}
