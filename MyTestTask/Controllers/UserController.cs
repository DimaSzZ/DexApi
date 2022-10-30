using Microsoft.AspNetCore.Mvc;
using MyTestTask.Data;
using Microsoft.AspNetCore.Authorization;
using MyTestTask.dto.User.Request;
using MyTestTask.Models;
using MyTestTask.Services.UserService;

namespace MyTestTask.Controllers
{
    [ApiController]
    [Route("Person")]
    public class UserController : Controller
    {
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ApplicationDbContext _db;
        private readonly IUserController _userController;

        public UserController(JwtAuthenticationManager jwt, ApplicationDbContext db,IUserController userController)
        {
            jwtAuthenticationManager = jwt;
            _db = db;
            _userController = userController;
        }
        [AllowAnonymous]
        [HttpPost("Authorize")]
        public async Task<IActionResult> AuthUser([FromBody] AuthorizationUserRequest AuthContext)
        {
            var context =  _userController.Authorization(_db,AuthContext,jwtAuthenticationManager);
            if (context == null)
                return BadRequest();
            return Ok(context);
        }
        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationUser([FromQuery] RegistrationUserRequest registrationUserRequest)
        {
            var personRequest = await _userController.Registration(_db, registrationUserRequest);
            return Ok(personRequest);
        }
    }
}
