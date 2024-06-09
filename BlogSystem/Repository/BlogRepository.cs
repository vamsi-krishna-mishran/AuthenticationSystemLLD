using BlogSystem.Context;
using BlogSystem.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Repository
{
    public interface IBlogRepository
    {
        public Task<List<Blog>> getBlogs();

        public Task<(Blog, string)> getBlog(int id);
        public Task<List<Blog>> getBlogsbyAdmin(string username);

        public Task<(Blog,string)> addBlog(Blog blog,string username);

        public Task<(Blog,string)> updateBlog(Blog blog);

        public Task<(bool,string)> removeBlog(int blogid);

        public Task<bool> rateBlog(Blog blog, Rating rating);


    }
    public class BlogRepository : IBlogRepository
    {

        private BlogDbContext _context;

        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<(Blog,string)> addBlog(Blog blog,string username)
        {
            try
            {

                (Blog, string) result = new();
                var user = await _context.users.Where(us => us.userName == username).FirstOrDefaultAsync();
                if (user != null)
                {
                    blog.adminId = user;
                    blog.publishDate = DateTime.Now;
                    var res = await _context.blog.AddAsync(blog);
                    await _context.SaveChangesAsync();
                    result=(res.Entity, "blog added.");
                }
                else
                {
                    result=(null, "Invalid User.");
                }
                return result;
                
            }
            catch (Exception ex)
            {
                return (null,ex.Message);
            }
        }


        public async Task<(Blog,string)> updateBlog(Blog b)
        {
            try
            {
                (Blog, string) result = new();
                Blog blog = await _context.blog.FindAsync(b.id);
                if (blog != null)
                {
                    blog.blogHeading = b.blogHeading;
                    blog.blogThumbnailImg = b.blogThumbnailImg;
                    blog.blogDocument = b.blogDocument;
                    await _context.SaveChangesAsync();
                    result = (blog, "updated successfully.");
                }
                else
                {
                    result = (null, "blog not found to update.");
                }
                return result;
            }
            catch(Exception ex)
            {
                return (null,ex.Message);
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
        public async Task<List<Blog>> getBlogsbyAdmin(string username)
        {
            try
            {
                var result = await _context.blog.Where(bg=>bg.adminId.userName==username).ToListAsync();
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
                   // blog.ratings.Add(rating);
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

        public async Task<(bool,string)> removeBlog(int blogid)
        {
            try
            {
                (bool, string) resultout = new();
                #region removeStale
                
                #endregion
                var r2 = await _context.blog.ToListAsync();
                var result = await _context.blog.FindAsync(blogid);
                if (result != null)
                {
                    _context.blog.Remove(result);
                    await _context.SaveChangesAsync();
                    resultout = (true, "blog deleted successfully.");
                }
                else
                    resultout = (false, "blog not found");
                return resultout;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(Blog,string)> getBlog(int id)
        {
            try
            {
                (Blog, string) result = new();
                Blog blog = await _context.blog.FindAsync(id);
                if (blog != null)
                {
                    result = (blog, "OK");
                }
                else
                {
                    result = (null, "blog not found");
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
