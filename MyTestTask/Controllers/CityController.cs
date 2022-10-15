using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using MyTestTask.Data;
using MyTestTask.dto.CityFolder.Request;
using MyTestTask.dto.CityFolder.Response;
using MyTestTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyTestTask.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CityController(ApplicationDbContext db)
        {
            _db = db;
        }
        [AllowAnonymous]
        [HttpPost("PostCity")]
        public async Task<IActionResult> Post([FromQuery]PostCityRequest context)
        {
            var ct = new Cities { City = context.City };
            if (ct == null)
            {
                return BadRequest();
            }

            Debug.Assert(_db.Cities != null, "_db.Cities != null");
            if (_db.Cities.Any(x => x.City == context.City))
            {
                return BadRequest("Такой город уже существует");
            }
            await _db.Cities!.AddAsync(ct);
            await _db.SaveChangesAsync();
            return Ok(new PostCityResponse { Message = $"Город {context.City} успешно создан" });
        }

        [AllowAnonymous]
        [HttpGet("GetAllCities")]
        public IActionResult GetAllCity()
        {
            Debug.Assert(_db.Cities != null, "_db.Cities != null");
            return Ok(new GetAllCitiesResponse { CityList = _db.Cities.ToList() });
        }

        [AllowAnonymous]
        [HttpGet("GetCity")]
        public Task<IActionResult> GetCity([FromQuery] GetCityRequest context)
        {
            Debug.Assert(_db.Cities != null, "_db.Cities != null");
            var result = _db.Cities.Where(x => x.City == context.City);
            return result.Any() ? Task.FromResult<IActionResult>(Ok(new GetCityResponse { City = context.City })) 
                : Task.FromResult<IActionResult>(BadRequest("Такого города нет в бд"));
        }

        [AllowAnonymous]
        [HttpDelete("DeleteCity")]
        public async Task<IActionResult> DeleteCity([FromQuery] DeleteCityRequest context)
        {
            Debug.Assert(_db.Cities != null, "_db.Cities != null");
            var result = _db.Cities.Where(x => x.City == context.City);
            if (!result.Any()) return BadRequest("Такого города нет в бд");
            _db.Remove(result.First());
            await _db.SaveChangesAsync();
            return Ok(new DeleteCityResponse{Message = $"Город {context.City} удален"});
        }

        [AllowAnonymous]
        [HttpPut("UpdateCity")]
        public async Task<IActionResult> UpdateCity([FromQuery] UpdateCityRequest context)
        {
            Debug.Assert(_db.Cities != null, "_db.Cities != null");
            var result = _db.Cities.Where(x => x.City == context.CityOld);
            if (!result.Any()) return BadRequest("Такого города нет в бд");
            result.First().City = context.CityNew;
            _db.Update(result.First());
            await _db.SaveChangesAsync();
            return Ok(new UpdateCityResponse{ Message = $"Город {context.CityOld} обновлен и теперь имеет название {context.CityNew}"});
        }
    }
}
