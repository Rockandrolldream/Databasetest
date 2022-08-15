using Databasetest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Databasetest.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IJWTManagerRepository _jWTManager;

        public UserController(IJWTManagerRepository jWTManager, BallingdatabaseContext context)
        {
            this._jWTManager = jWTManager;
        }

        [HttpGet]
        [Authorize]
        [Route("Getusers")]
        public List<String> Get()
        {
            var users = new List<string>
        {
            "Satinder Singh",
            "Amit Sarna",
            "Davin Jon"
        };
            return users;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(User usersdata)
        {
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

    }
}
