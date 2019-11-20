﻿using App.DataAccessLayer.AppContext;
using App.DataAccessLayer.Entities;
using App.DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<User> Create(User item)
        {
            await _context.Users.AddAsync(item);
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
        public async Task<User> Read(Guid Id)
        {
            User user = await _userManager.FindByIdAsync(Id.ToString());
            return user;
        }
        public async Task<User> Update(User item)
        {
            _context.Users.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public List<IdentityUser> GetAll()
        {
            List<IdentityUser> users = _context.Users.ToList();
            return users;
        }
    }
}
