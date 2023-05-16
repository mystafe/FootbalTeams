using FootbalTeams.Contexts;
using FootbalTeams.Models.DTO.CityDto;
using FootbalTeams.Models.DTO.TeamDto;
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
            List<CityResponseDto> results = context.Cities.Include(c=>c.Teams).Include(c=>c.Country).Select(c=>new CityResponseDto() 
            { CityName=c.Name, 
                CountryName=c.Country!=null?c.Country.Name:String.Empty,
                TeamNames= c.Teams==null?null:
                                         c.Teams.Select(t=>new TeamNameDto(){TeamName=t.Name}
                ).ToList()
            }).ToList();            
            if (results != null)
            {

                return Ok(results);

            }
            else return NotFound();
        }

        


        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            City city = context.Cities.Include("Country").AsQueryable().FirstOrDefault(c=>c.Id==id);
            if (city != null)
            {
                CityResponseDto result = new CityResponseDto()
                { CityName =city.Name,
                    CountryName=city.Country!=null? city.Country.Name:String.Empty,
                   TeamNames= city.Teams==null?null:city.Teams.Select(t=>new TeamNameDto() { TeamName=t.Name}).ToList()
                };
                return Ok(result);
            }
            else return NotFound();
        }

        [HttpPost]
        public IActionResult Post( CityCreateDTO model)
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
