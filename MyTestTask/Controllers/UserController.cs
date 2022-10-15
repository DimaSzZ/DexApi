using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyTestTask.Data;
using Microsoft.AspNetCore.Authorization;
using MyTestTask.dto.UserFolder.Request;
using MyTestTask.dto.UserFolder.Response;
using MyTestTask.Models;

namespace MyTestTask.Controllers
{
    [ApiController]
    [Route("Person")]
    public class UserController : Controller
    {
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private readonly ApplicationDbContext _db;

        public UserController(JwtAuthenticationManager jwt, ApplicationDbContext db)
        {
            jwtAuthenticationManager = jwt;
            _db = db;
        }
        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody] AuthorizationUserRequest context)
        {
            var token = jwtAuthenticationManager.Authenticate(context.Name,context.Password,context.Number,_db);
            if (token == null)
            {
                return Unauthorized();
            }

            Debug.Assert(_db.Persons != null, "_db.Persons != null");
            var Id = _db.Persons.First(x =>
                    x.Name == context.Name && x.Number == context.Number && x.Password == context.Password);

            Response.Cookies.Append("Id",$"{Id.Id}");
            if (_db.Persons == null) return Ok(new AuthorizationUserResponse { Message = $"{token}" });
            var id = _db.Persons.First(x => x.Name == context.Name && x.Number == context.Number && x.Password == context.Password);

            return Ok(new AuthorizationUserResponse{Message = $"{token}"});
        }
        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IActionResult> RegistrationUser([FromQuery] RegistrationUserRequest context)
        {
            var ct = new  Person{ Name = context.Name,Password = context.Password,Number = context.Number};
            if (ct == null)
            {
                return BadRequest();
            }

            Debug.Assert(_db.Persons != null, "_db.Persons != null");
            if (_db.Persons.Any(x => x.Name == ct.Name && x.Password == ct.Password && x.Number == ct.Number))
            {
                return BadRequest("Такой пользователь уже зарегистрирован уже существует");
            }
            await _db.Persons!.AddAsync(ct);
            await _db.SaveChangesAsync();
            return Ok(new RegistrationUserResponse { Message = $"Name:{ct.Name} Password:{ct.Password} Number:{ct.Number}. Пользователь успешно создан." });
        }
    }
}
