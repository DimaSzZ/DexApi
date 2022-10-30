//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MyTestTask.Data;
//using MyTestTask.dto.Search.Request;
//using MyTestTask.dto.Search.Response;
//using MyTestTask.Models;

//namespace MyTestTask.Controllers
//{
//    [ApiController]
//    [Route("SearchController")]
//    public class SearchController : Controller
//    {
//        private readonly ApplicationDbContext _db;

//        public SearchController(ApplicationDbContext db)
//        {
//            _db = db;
//        }
//        [AllowAnonymous]
//        [HttpPost("Search")]
//        public async Task<ActionResult<List<SearchCriteriaResponse>>> Search([FromQuery] SearchCriteriaRequest ct)
//        {
//            if (ct != null)
//            {
//                if (_db.Advertisings != null)
//                {
//                    var allAdvert = _db.Advertisings.Select(ct => new SearchCriteriaResponse
//                        {
//                            Name = ct.Name,
//                            Price = ct.Price,
//                            Number = ct.Number,
//                            City = ct.City,
//                            Category = ct.Category,
//                            Subcategory = ct.Subcategory,
//                            Description = ct.Description,
//                            PublicationDate = ct.PublicationDate
//                        })
//                        .ToList();
//                    return Ok(allAdvert);
//                }
//            }

//            var preliminary = _db.Advertisings;
//            if (_db.Cities != null && !_db.Cities.Any(x => x.City == ct!.City))
//            {
//                return BadRequest("Такого города нет в бд");
//            }
//            else
//            {
//                if (preliminary != null)
//                {
//                    preliminary = (DbSet<Ad>)preliminary.Where(x => ct != null && x.City == ct.City);
//                }
//            }
//            if (_db.Categories != null && !_db.Categories.Any(x => x.Category == ct!.Category))
//            {
//                return BadRequest("Такой категории не существует");
//            }
//            else
//            {
//                if (preliminary != null)
//                {
//                    preliminary = (DbSet<Ad>)preliminary.Where(x => ct != null && x.Category == ct.Category);
//                }
//            }
//            if (_db.Subcategories != null && !_db.Subcategories.Any(x => x.Subcategory == ct!.SubCategory))
//            {
//                return BadRequest("Такой подкатегории  не существует");
//            }
//            else
//            {
//                if(preliminary != null)
//                {
//                    preliminary = (DbSet<Ad>)preliminary.Where(x => ct != null && x.Subcategory == ct.SubCategory);
//                }
//            }
            
//            var finalResponse = new List<SearchCriteriaResponse>();
//            if (preliminary == null) return Ok();
//            foreach (var item in preliminary)
//            {
//                finalResponse.Add(new SearchCriteriaResponse
//                {
//                    Name = item.Name,
//                    Price = item.Price,
//                    Number = item.Number,
//                    City = item.City,
//                    Category = item.Category,
//                    Subcategory = item.Subcategory,
//                    Description = item.Description,
//                    PublicationDate = item.PublicationDate
//                });
//            }
//            return Ok(finalResponse);
//        }
//    }
//}
