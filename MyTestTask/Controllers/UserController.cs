using Microsoft.AspNetCore.Mvc;
using MyTestTask.Data;
using Microsoft.AspNetCore.Authorization;
using MyTestTask.dto.User.Request;
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
        private readonly ILogger<AdvertisementController> _logger;

        public UserController(JwtAuthenticationManager jwt, ApplicationDbContext db,IUserController userController, ILogger<AdvertisementController> logger)
        {
            jwtAuthenticationManager = jwt;
            _db = db;
            _userController = userController;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpPost("Authorize")]
        public async Task<IActionResult> AuthUser([FromBody] AuthorizationUserRequest AuthContext)
        {
            _logger.LogInformation($"Запрос на авторизацию был отправлен {DateTime.Now:hh:mm:ss}");
            var context =  _userController.Authorization(_db,AuthContext,jwtAuthenticationManager);
            if (context == null)
                return BadRequest();
            return Ok(context);
        }
        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationUser([FromQuery] RegistrationUserRequest registrationUserRequest)
        {
            _logger.LogInformation($"Запрос на регистрацию был отправлен {DateTime.Now:hh:mm:ss}");
            var personRequest = await _userController.Registration(_db, registrationUserRequest);
            if (personRequest == null)
                return BadRequest();
            return Ok(personRequest);
        }
    }
}
