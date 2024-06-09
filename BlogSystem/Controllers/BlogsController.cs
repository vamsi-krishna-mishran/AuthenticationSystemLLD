using AutoMapper;
using BlogSystem.Context;
using BlogSystem.DTOs;
using BlogSystem.Models;
using BlogSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private IBlogRepository _repo;
        private readonly IMapper _mapper;
        public BlogsController(IBlogRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get3(int id)
        {
            try
            {
                var result = await _repo.getBlog(id);
                if(result.Item1 == null)
                {
                    throw new Exception(result.Item2);
                }
                return Ok(result.Item1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("all/admin")]
        public async Task<IActionResult> Get2()
        {
            try
            {
                var user = HttpContext.User;
                string username = user.FindFirstValue(ClaimTypes.Name);

                var result = await _repo.getBlogsbyAdmin(username);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="ADMIN")]
        // GET api/<BlogController>/5
        [HttpPost("addblog")]
        public async Task<IActionResult> post(BlogDTO blog)
        {
            try
            {
                Blog dblog = _mapper.Map<Blog>(blog);
                var user = HttpContext.User;
                string username = user.FindFirstValue(ClaimTypes.Name);
                
                var res=await _repo.addBlog(dblog,username);
                if (res.Item1 == null)
                {
                    throw new Exception(res.Item2);
                }
                return Ok(res.Item1);
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
                var res=await _repo.removeBlog(id);
                if (res.Item1) return Ok(id);
                return BadRequest(res.Item2);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> PUT(BlogDTO blog)
        {
            try
            {
                Blog bblog= _mapper.Map<Blog>(blog);    
                var result=await _repo.updateBlog(bblog);
                if (result.Item1 == null)
                {
                    throw new Exception(result.Item2);
                }
                return Ok(result.Item1);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        

        
    }
}
