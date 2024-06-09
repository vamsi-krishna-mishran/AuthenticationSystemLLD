using BlogSystem.Enums;
using BlogSystem.Models;
using BlogSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _repo;
        private IConfiguration _config;

        private IBlogRepository _brepo;
        public UserController(IUserRepository repo,IConfiguration config, IBlogRepository brepo)
        {
            _repo = repo;
            _config = config;
            _brepo = brepo;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                bool result = await _repo.register(user);
                if (result)
                {
                    return Ok($"{user}");
                }
                else
                {
                    return BadRequest($"user already registered");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("registration Failed");
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Post2(string uname,string pwd)
        {
            User result=await _repo.login(uname, pwd);
            if (result!=null)
            //if(true)
            {
                //CookieOptions options=new CookieOptions();
                //options.Expires = DateTime.UtcNow.AddMinutes(30);
                var options = new CookieOptions()
                {
                   // Path = "/",
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    IsEssential = true,
                    HttpOnly = false,
                    Secure = false,
                };
                string key = _config["Jwt:Key"];
                string issuer = _config["Jwt:Issuer"];
                int liveTime = Convert.ToInt16(_config["Jwt:LiveTime"]);
                string token=JWT.generateToken(result.userType,key, issuer, liveTime,result.userName);


                Response.Cookies.Append("token", token, options);
                return Ok("Login success.");
            }
            return Unauthorized();

        }


        [Authorize]
        [HttpGet("resource1")]
        public  IActionResult Get2()
        {
            var user = HttpContext.User;
            var role = user.FindFirstValue(ClaimTypes.Name);
            var name = user.FindFirstValue(ClaimTypes.Role);
            
            return Ok("got the resource1");
        }

        [Authorize(Roles ="ADMIN")]
        [HttpGet("resource2")]
        public IActionResult Get3()
        {
            return Ok("got the resource2");
        }

        [HttpGet("resetPassword")]
        public async Task<IActionResult> reset(string uname,string pwd,string newpwd)
        {
            try
            {
                bool res = await _repo.resetPassword(uname, pwd, newpwd);
                if (res)
                {
                    return Ok($"new password is {newpwd}");
                }
                return BadRequest("Invalid credentials.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("rateblog")]
        public async Task<IActionResult> rate([FromBody] Blog blog,[FromQuery]int rating)
        {
            try
            {
                Rating r = new Rating();
                r.rating = rating;
                var res=await _brepo.rateBlog(blog,r);
                if (res) return Ok("rated");
                return BadRequest("failed to rate");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> logout()
        {
            Response.Cookies.Delete("token");
            return Ok("removed cookie");
        }
    }
}
