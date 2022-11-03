using Microsoft.AspNetCore.Mvc;

namespace MyTestTask.Controllers
{
    [ApiController]
    [Route("MidlwareTestController")]
    public class MidlwareTestController : Controller
    {
        [HttpPost("Error")]
        public string Index()
        {
           return("Секретная информация");
        }
    }
}
