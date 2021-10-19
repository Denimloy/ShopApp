using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.ViewModels;

namespace ShopApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext appDbContext)
        {
            this._db = appDbContext;
        }
        public async Task CreateAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }
        public async Task<bool> CheckAddressForAvailabilityAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            if(user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
