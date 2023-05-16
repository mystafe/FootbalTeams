using FootbalTeams.Contexts;
using FootbalTeams.Models.DTO.CountryDto;
using FootbalTeams.Models.ORM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            List<CountryResponseDto> results = context.Countries.Include(c=>c.Cities).Select(c=> new CountryResponseDto() 
            { 
                CountryName=c.Name, 
                CityNames=c.Cities==null ? null : c.Cities.Select(ct=>new CityNameDto() { CityName=ct.Name}).ToList()
            }
            ).ToList();
            if (results.Count>0)
            {
                return Ok(results);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Country country=context.Countries.Find(id);
  
            if (country != null)
            {
                CountryResponseDto result = new CountryResponseDto() { CountryName = country.Name, CityNames = country.Cities.Select(c => new CityNameDto() { CityName = c.Name }).ToList() };

                return Ok(result);
            }
            else
                return NotFound();   
        }

        [HttpPost]
        public IActionResult CreateCounty(CountryCreateDTO model) {
        
            if (ModelState.IsValid&&model!=null) {
                Country country = new Country() { Name = model.Name };
                context.Countries.Add(country);
                context.SaveChanges();
                CountryResponseDto result = new CountryResponseDto()
                {
                    CountryName = country.Name,
                    CityNames = country.Cities.Select(c => new CityNameDto() { CityName = c.Name }).ToList()
                };
                return Ok(result);

            }
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
            CountryResponseDto result = new CountryResponseDto();

            result.CountryName =country.Name;
            context.Countries.Remove(country);
            context.SaveChanges();
            return Ok(result);
          
        }
    }
}
