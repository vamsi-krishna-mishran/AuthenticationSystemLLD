using BlogSystem.Context;
using BlogSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Repository
{
    public interface IBlogRepository
    {
        public Task<List<Blog>> getBlogs();

        public Task<bool> addBlog(Blog blog);

        public Task<bool> removeBlog(Blog blog);

        public Task<bool> rateBlog(Blog blog, Rating rating);

    }
    public class BlogRepository : IBlogRepository
    {

        private BlogDbContext _context;

        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<bool> addBlog(Blog blog)
        {
            try
            {
                await _context.blog.AddAsync(blog);
                return true;
            }
            catch (Exception ex)
            {
                return false;
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

        public async Task<bool> removeBlog(Blog blog)
        {
            try
            {
                if (await _context.blog.FindAsync(blog) != null)
                {
                    _context.blog.Remove(blog);
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
