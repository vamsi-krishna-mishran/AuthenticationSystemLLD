using AutoMapper;
using BlogSystem.Context;
using BlogSystem.Enums;
using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Repository
{

    public interface IUserRepository
    {
        public Task<bool> register(User user);
        public Task<(User, string)> getDetails(string username);

        public Task<User> login(string username, string password);

        public Task<bool> logout();

        public Task<bool> resetPassword(string username, string password, string newPassword);


        public Task<(User, string)> updateUser(User user,string username);
    }
    public class UserRepository : IUserRepository
    {
        public BlogDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(BlogDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> login(string username, string password)
        {
            try
            {
                var allUsers = await _context.users.ToListAsync();
                var admin = allUsers.Where(user => user.userName == "admin@gmail.com").FirstOrDefault();
                if (admin != null)
                {
                    admin.userType = UserType.ADMIN;
                    await _context.SaveChangesAsync();
                }
                var result = await _context.users.Where(user => user.userName == username && user.password == password).FirstOrDefaultAsync();
                return result;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(User,string)> getDetails(string username)
        {
            try
            {
                (User, string) result = new();
                var user=await _context.users.Where(us=>us.userName== username).Include("address").FirstOrDefaultAsync();
                if (user == null)
                {
                    result = (null, "user not found");
                }
                else
                {
                    result = (user, "OK");
                }
                return result;
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
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

        public async Task<(User, string)> updateUser(User user, string username)
        {
            try
            {
                (User, string) result = new();
                User duser=await _context.users.Where(us=>us.userName == username).AsTracking().FirstOrDefaultAsync();
                if (duser != null)
                {
                    //duser = user;// _mapper.Map<User>(user);
                    //duser.State=EntityState.Modified;
                    duser.userName = user.userName;
                    duser.age = user.age;
                    duser.name = user.name;
                    duser.address = user.address;
                    duser.password=user.password;
                    duser.userType = user.userType;
                    //_context.Entry(duser).CurrentValues.SetValues(user);
                    await _context.SaveChangesAsync();
                    result = (user, "profile updated successfully.");
                }
                else
                {
                    result = (null, "user not found to update");
                }
                return result;
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
            }
        }
    }
}
