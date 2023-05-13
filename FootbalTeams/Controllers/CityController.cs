using FootbalTeams.Contexts;
using FootbalTeams.Models.DTO;
using FootbalTeams.Models.ORM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FootbalTeams.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        private readonly FootbalContext context;
        public CityController(FootbalContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult GetCities()
        {
            List<City> cities = context.Cities.AsQueryable().ToList();            
            if (cities != null)
            {
                return Ok(cities);

            }
            else return NotFound();
        }


        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            City city = context.Cities.Find(id);
            if (city != null)
            {
                return Ok(city);
            }
            else return NotFound();
        }

        [HttpPost]
        public IActionResult Post(CityCreateDTO model)
        {
            City city = new City();
            city.CountryId = model.CountryId;
            city.Name = model.Name;
            if (model != null)
            {
                context.Cities.Add(city);
                context.SaveChanges();
                return Ok(city);
            }
            else
            {
                return BadRequest();
            }

        }


        [HttpPut("{id}")]
        public IActionResult Delete(int id, [FromBody] CityCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                City city = context.Cities.Find(id);
                if (city != null)
                {
                    city.CountryId = model.CountryId;
                    city.Name=model.Name;
                    context.SaveChanges();
                    return Ok(city);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            City city = context.Cities.Find(id);
            if (city != null)
            {
                context.Cities.Remove(city);
                context.SaveChanges();
                return Ok(city);
            }
            return NotFound();

        }
    }
    
}
