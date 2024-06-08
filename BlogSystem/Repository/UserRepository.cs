using BlogSystem.Context;
using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Repository
{

    public interface IUserRepository
    {
        public Task<bool> register(User user);

        public Task<User> login(string username, string password);

        public Task<bool> logout();

        public Task<bool> resetPassword(string username, string password, string newPassword);
    }
    public class UserRepository : IUserRepository
    {
        public BlogDbContext _context;

        public UserRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<User> login(string username, string password)
        {
            try
            {
                var allUsers = await _context.users.ToListAsync();
                var result = await _context.users.Where(user => user.userName == username && user.password == password).FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> logout()
        {
            await Task.Delay(100);
            return true;
        }

        public async Task<bool> register(User user)
        {
            var result = await _context.users.Where(us => us.userName == user.userName).FirstOrDefaultAsync();
            if (result == null)
            {
                await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> resetPassword(string username, string password, string newPassword)
        {
            try
            {
                var result = await _context.users.Where(user => user.userName == username && user.password == password).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.password = newPassword;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
