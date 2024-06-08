using BlogSystem.Context;
using BlogSystem.Models;
using BlogSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private IBlogRepository _repo;

        public BlogsController(IBlogRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<BlogController>
        [HttpGet("all")]
        public async Task<IActionResult>  Get()
        {
            try
            {
                var result = await _repo.getBlogs();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="ADMIN")]
        // GET api/<BlogController>/5
        [HttpPost("addblog")]
        public async Task<IActionResult> post(Blog blog)
        {
            try
            {
                var res=await _repo.addBlog(blog);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        // POST api/<BlogController>
        [HttpDelete("removeblog")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool res=await _repo.removeBlog(id);
                if (res) return Ok(id);
                return BadRequest(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        

        
    }
}
