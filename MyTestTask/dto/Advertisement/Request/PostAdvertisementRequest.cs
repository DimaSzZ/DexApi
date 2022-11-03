namespace MyTestTask.dto.Advertisement.Request
{
    ///<summary>
    ///Дто для отправки запроса на добавление объявления
    ///</summary>
    public class PostAdvertisementRequest
    {
        public string Number { get; set; }
        public string? Description { get; set; }
        public string Page { get; set; }
        public string Rating { get; set; }
    }
}
