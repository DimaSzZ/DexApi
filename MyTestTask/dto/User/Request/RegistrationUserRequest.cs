namespace MyTestTask.dto.User.Request
{
    ///<summary>
    ///Дто для отправки запроса на регистрацию аккаунта
    ///</summary>
    public class RegistrationUserRequest
    {
        public string? Name { get; set; }
        public bool Admin { get; set; }
    }
}
