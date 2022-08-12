using Databasetest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Databasetest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly BallingdatabaseContext _context;

        public UserController(IJWTManagerRepository jWTManager, BallingdatabaseContext context)
        {
            this._jWTManager = jWTManager;
            _context = context;
        }

        [HttpGet("GetUsers")]
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

        [HttpPost("CreateToken")]
        public IActionResult Authenticate(User usersdata)
        {
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }
            _context.Tokens.Add(token);
            return Ok(token);
        }

    }
}
