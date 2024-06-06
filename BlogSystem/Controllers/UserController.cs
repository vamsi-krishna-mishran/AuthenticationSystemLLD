using BlogSystem.Models;
using BlogSystem.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }
        
        
        [HttpPost("register")]
        public async Task<IActionResult> Post(User user)
        {
            bool result=await _repo.register(user);
            if (result)
            {
                return Ok($"{user}");
            }
            return StatusCode(400, "registration Failed");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post2(string uname,string pwd)
        {
            bool result=await _repo.login(uname, pwd);
           // if (result)
            if(true)
            {
                CookieOptions options=new CookieOptions();
                options.Expires = DateTime.UtcNow.AddMinutes(1);
                Response.Cookies.Append("token", "x01r5", options);
                return Ok("Login success.");
            }
            return StatusCode(400, "authentication Failed");

        }
    }
}
