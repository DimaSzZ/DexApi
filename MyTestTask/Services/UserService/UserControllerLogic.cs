using MyTestTask.Data;
using MyTestTask.dto.User.Request;
using MyTestTask.Models;

namespace MyTestTask.Services.UserService
{
    ///<summary>
    ///Это сервис для реализации логики всех апи в UserController
    ///</summary>
    public class UserControllerLogic : IUserController
    {
        private readonly IHttpContextAccessor _contextAccessor;

        ///<summary>
        ///В конструкторе сново принимается contextAccessor, для доступа к куки
        ///</summary>
        public UserControllerLogic(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        ///<summary>
        ///Метод, реализующий логику для регистрации новго пользователя
        ///</summary>
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
        ///<summary>
        ///Метод, реализующий логику для регистрации новго пользователя
        ///</summary>
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
