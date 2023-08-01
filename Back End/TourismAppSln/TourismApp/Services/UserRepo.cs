//using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TourismApp.Interfaces;
using TourismApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TourismApp.Services
{
    public class UserRepo : IRepo<string, User> 
    {
        private readonly Context _context;
        private readonly ILogger<UserRepo> _logger;

        public UserRepo(Context context, ILogger<UserRepo> logger)
        {
            _context=context;
            _logger=logger;
        }
        public async Task<User?> Add(User item)
        {

            try
            {
                _context.Users.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        } 

        public async Task<User?> Delete(string key)
        {
            try
            {
                var user = await Get(key);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<User?> Get(string key)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == key);
                return user;
            }  
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            } 
            return null;
        }

        public async Task<ICollection<User>?> GetAll()
        {

            try
            {
                var users = await _context.Users.ToListAsync();
                if (users.Count > 0)
                    return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<User?> Update(User item)
        {
            try
            {
                var user = await Get(item.Email);
                if (user != null)
                {
                    user.Role = item.Role;
                    user.PasswordHash = item.PasswordHash;
                    user.PasswordKey = item.PasswordKey;
                    await _context.SaveChangesAsync();
                    return user;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    
    }
}
