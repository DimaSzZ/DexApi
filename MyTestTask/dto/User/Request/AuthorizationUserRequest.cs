namespace MyTestTask.dto.User.Request
{
    ///<summary>
    ///Дто для отправки запроса на авторизацию аккаунта
    ///</summary>
    public class AuthorizationUserRequest
    {
        public string? Name { get; set; }
        public bool Admin { get; set; }
        
    }
}
