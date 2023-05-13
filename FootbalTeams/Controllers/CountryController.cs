using FootbalTeams.Contexts;
using FootbalTeams.Models.DTO;
using FootbalTeams.Models.ORM;
using Microsoft.AspNetCore.Mvc;

namespace FootbalTeams.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : Controller
    {
        private readonly FootbalContext context;
        public CountryController(FootbalContext _context) {
            context = _context;
        }

        [HttpGet]
        public IActionResult  GetCountries()
        {

            List<Country> countries = context.Countries.AsQueryable().ToList();
            if (countries.Count>0)
            {
                return Ok(countries);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Country country=context.Countries.Find(id);
  
            if (country != null)
            {
                return Ok(country);
            }
            else
                return NotFound();   
        }

        [HttpPost]
        public IActionResult CreateCounty(CountryCreateDTO model) {
        
            Country country=new Country() { Name = model.Name };
            context.Countries.Add(country);
            context.SaveChanges();
            if (model!=null) return Ok(country);
            else return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id , [FromBody] CountryCreateDTO model ) {
          
            if (ModelState.IsValid) {
                Country country = context.Countries.Find(id);
                if (country!=null)
                {
                       country.Name = model.Name;
                    context.SaveChanges();
                    return Ok(country);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Country country = context.Countries.Find(id);
            if (country == null)    return NotFound();      
            context.Countries.Remove(country);
            context.SaveChanges();
            return Ok(country);
          
        }
    }
}
