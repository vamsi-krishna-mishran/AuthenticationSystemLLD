using BlogSystem.Context;
using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Repository
{
    public interface IBlogRepository
    {
        public Task<List<Blog>> getBlogs();

        public Task<Blog> addBlog(Blog blog);

        public Task<bool> removeBlog(int blogid);

        public Task<bool> rateBlog(Blog blog, Rating rating);

    }
    public class BlogRepository : IBlogRepository
    {

        private BlogDbContext _context;

        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<Blog> addBlog(Blog blog)
        {
            try
            {
                var res=await _context.blog.AddAsync(blog);
                await _context.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Blog>> getBlogs()
        {
            try
            {
                var result = await _context.blog.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> rateBlog(Blog blog, Rating rating)
        {
            try
            {
                if (await _context.blog.FindAsync(blog) != null)
                {
                    blog.ratings.Add(rating);
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

        public async Task<bool> removeBlog(int blogid)
        {
            try
            {
                var result = await _context.blog.FindAsync(blogid);
                if (result != null)
                {
                    _context.blog.Remove(result);
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
