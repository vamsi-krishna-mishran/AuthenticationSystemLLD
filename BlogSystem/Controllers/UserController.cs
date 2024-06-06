using BlogSystem.Enums;
using BlogSystem.Models;
using BlogSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _repo;
        private IConfiguration _config;

        public UserController(IUserRepository repo,IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }


        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Post2(string uname,string pwd)
        {
            User result=await _repo.login(uname, pwd);
            if (result!=null)
            //if(true)
            {
                CookieOptions options=new CookieOptions();
                options.Expires = DateTime.UtcNow.AddMinutes(30);
                string key = _config["Jwt:Key"];
                string issuer = _config["Jwt:Issuer"];
                int liveTime = Convert.ToInt16(_config["Jwt:LiveTime"]);
                string token=JWT.generateToken(result.userType,key, issuer, liveTime);


                Response.Cookies.Append("token", token, options);
                return Ok("Login success.");
            }
            return Unauthorized();

        }


        [Authorize]
        [HttpGet("resource1")]
        public  IActionResult Get2()
        {
            return Ok("got the resource1");
        }

        [Authorize(Roles ="ADMIN")]
        [HttpGet("resource2")]
        public IActionResult Get3()
        {
            return Ok("got the resource2");
        }
    }
}
