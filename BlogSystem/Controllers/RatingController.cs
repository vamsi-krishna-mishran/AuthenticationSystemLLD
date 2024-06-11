using AutoMapper;
using BlogSystem.DTOs;
using BlogSystem.Models;
using BlogSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _repo;

        private readonly IMapper _mapper;
        
        public RatingController(IRatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            
        }
        
        // GET: api/<RatingController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RatingController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("getrating/{id}")]
        public async Task<IActionResult> Get2(int id)
        {
            try
            {
                var user = HttpContext.User;
                string username = user.FindFirstValue(ClaimTypes.Name);
                var result = await _repo.getRatingWithUser(id, username);
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

        // POST api/<RatingController>
        [HttpPost("addrating")]
        public async Task<IActionResult> Post([FromBody] RatingDTO rating)
        {
            try
            {
                Rating Drating = _mapper.Map<Rating>(rating);
                var user = HttpContext.User;
                string username = user.FindFirstValue(ClaimTypes.Name);
                var result = await _repo.addRating(Drating, username, rating.blogId);
                if (result.Item1 == null)
                {
                    throw new Exception(result.Item2);
                }
                return Ok(result.Item2);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<RatingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RatingController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
