using Microsoft.AspNetCore.Mvc;
using MyTestTask.Data;
using MyTestTask.dto.Advertisement.Request;
using MyTestTask.dto.User.Request;
using MyTestTask.Models;

namespace MyTestTask.Services.UserService
{
    public class UserControllerLogic : IUserController
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserControllerLogic(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public async Task<Person> Registration(ApplicationDbContext _db, RegistrationUserRequest registrationUserRequest)
        {
            var personRequest = new Person { Name = registrationUserRequest.Name, Admin = registrationUserRequest.Admin };
            if (personRequest == null)
            {
                return null;
            }
            if (_db.Persons.Any(x => x.Name == personRequest.Name))
            {
                return null;
            }
            await _db.Persons!.AddAsync(personRequest);
            await _db.SaveChangesAsync();
            return personRequest;
        }

        public string? Authorization(ApplicationDbContext _db, AuthorizationUserRequest AuthContext,
            JwtAuthenticationManager jwtAuthenticationManager)
        {
            var token = jwtAuthenticationManager.Authenticate(AuthContext.Name, AuthContext.Admin, _db);
            if (token == null)
            {
                return null;
            }
            var Id = _db.Persons.First(x =>
                x.Name == AuthContext.Name && x.Admin == AuthContext.Admin);
            _contextAccessor.HttpContext?.Response.Cookies.Append("Id", $"{Id.Id}");
            return token;
        }
    }
}
