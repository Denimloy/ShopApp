using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> CreateAsync(User user);
        Task<bool> CheckEmailForAvailabilityAsync(string email);
        Task<bool> CheckLoginDetailsAsync(string email, string password);
        Task<User> GetUserAsync(string email);
    }
}
