using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.ViewModels;

namespace ShopApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger _logger;
        public UserRepository(AppDbContext appDbContext, ILogger<UserRepository> logger)
        {
            this._logger = logger;
            this._db = appDbContext;
        }
        public async Task<bool> CreateAsync(User user)
        {
            try
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CreateAsync method error");

                return false;
            }
        }
        public async Task<bool> CheckEmailForAvailabilityAsync(string email)
        {
            try
            {
                var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email.ToLower());
                if (user == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CheckEmailForAvailabilityAsync method error");

                return false;
            }
        }

        public async Task<bool> CheckLoginDetailsAsync(string email, string password)
        {
            try
            {
                var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email.ToLower() && x.Password == password);
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CheckLoginDetailsAsync method error");

                return false;
            }
        }

        public async Task<User> GetUserAsync(string email)
        {
            try
            {
                return await _db.Users.AsNoTracking().Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email.ToLower());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetUserAsync method error");

                return null;
            }
        }
    }
}
