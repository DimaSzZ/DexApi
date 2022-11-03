using MyTestTask.Data;
using MyTestTask.dto.User.Request;
using MyTestTask.Models;

namespace MyTestTask.Services.UserService
{
    ///<summary>
    ///Интерфейс, созданный для UserControllerLogic
    ///</summary>
    public interface IUserController
    {
        Task<Person> Registration(ApplicationDbContext _db, RegistrationUserRequest CreateAdcontext);
        string? Authorization(ApplicationDbContext _db, AuthorizationUserRequest AuthContext, JwtAuthenticationManager jwtAuthenticationManager);
    }
}
