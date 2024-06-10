using BlogSystem.Context;
using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BlogSystem.Repository
{
    
    public interface IRatingRepository
    {
        public Task<(Rating, string)> addRating(Rating rating,string username,int blogid);

        
    }

    public class RatingRepository : IRatingRepository
    {
        private readonly BlogDbContext _context;
        public RatingRepository(BlogDbContext context)
        {
            _context = context;
        }
        
        public  async Task<(Rating, string)> addRating(Rating rating, string username,int blogid)
        {
            try
            {
                (Rating, string) result = new();
                var Drating = await _context.rating.FindAsync(rating.id);
                if (Drating == null)
                {
                    User user = await _context.users.Where(us => us.userName == username).FirstOrDefaultAsync();
                    Blog blog=await _context.blog.FindAsync(blogid);
                    if (user == null && blog==null)
                    {
                        result = (null, "user and blog not found to rate.");
                    }
                    else if (user == null)
                    {
                        result = (null, "user not found to rate.");
                    }
                    else if (blog == null)
                    {
                        result = (null, "blog not found to rate.");
                    }
                    else
                    {
                        rating.user = user;
                        rating.blog = blog;
                        var res = await _context.rating.AddAsync(rating);
                        await _context.SaveChangesAsync();
                        result = (res.Entity, "Rating added successfully.");
                    }
                    return result;
                    
                }
                else
                {
                    Drating.rating = rating.rating;
                    await _context.SaveChangesAsync();
                    result = (Drating, "Rating updated successfully.");
                }
                return result;
            }
            catch(Exception ex)
            {
                return (null,ex.Message);
            }
        }
    }
}
