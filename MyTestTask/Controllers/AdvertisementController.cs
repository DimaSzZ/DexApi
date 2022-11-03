using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTestTask.Data;
using MyTestTask.dto.Advertisement.Request;
using MyTestTask.dto.Advertisement.Response;
using MyTestTask.Services.AdService;

namespace MyTestTask.Controllers
{
    [ApiController]
    [Route("Advertisement")]
    public class AdvertisementController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;
        private readonly IAdController _adController;
        public AdvertisementController(ApplicationDbContext db, IAdController adController, IMapper mapper)
        {
            _db = db;
            _adController = adController;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("CreateAd")]
       
        public async Task<IActionResult> AddAd([FromBody] PostAdvertisementRequest CreateAdcontext)
        {
            var response = await _adController.PushAd(_db, CreateAdcontext);
            if(response == null)
                return BadRequest();
            return Ok(response);
        }
        [Authorize]
        [HttpPut("UpdateAd")]
        
        public async Task<IActionResult> UpdateAd([FromBody] UpdateYourAdvertisementRequest UpdateContext)
        {
            var response = await _adController.UpdateAd(_db, UpdateContext);
            if (response == null)
                return BadRequest();
            return Ok(response);
        }
        [Authorize]
        [HttpDelete("DeleteAd")]

        public async Task<IActionResult> DeleteAd([FromBody] DeleteYourAdvertisementRequest DeleteContext)
        {
            var response = await _adController.DeleteAd(_db, DeleteContext);
            if (response == null)
                return BadRequest();
            return Ok(response);
        }
        [Authorize]
        [HttpGet("GetYourAllAd")]

        public async Task<IActionResult> GetYourAdvertisements()
        {
            var zxc = await _adController.GetAllAd(_db);
            var response = new List<GetYourAdvertisementResponse>();
            if (response == null)
                return BadRequest();
            foreach (var item in zxc)
            {
                response.Add(_mapper.Map<GetYourAdvertisementResponse>(item));
            }
            return Ok(response);
        }
    }
}

