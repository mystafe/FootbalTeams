using FootbalTeams.Contexts;
using FootbalTeams.Models.DTO.StadiumDto;
using FootbalTeams.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace FootbalTeams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumController : ControllerBase
    {
        private readonly FootbalContext context;
        public StadiumController(FootbalContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public IActionResult GetStadiums()
        {
            List<StadiumResponseDTO> results= context.Stadiums.Include("City").Include("Team").Select(s=>new StadiumResponseDTO() { StadiumName=s.Name,
                CityName=s.City!=null?s.City.Name:String.Empty,
                 TeamName= s.Team!=null?s.Team.Name: String.Empty
            }).ToList();

            if (results.Count > 0)
                return Ok(results);
            else return BadRequest();
        }
        [HttpGet("{id}")]
        public IActionResult GetStadiumById(int id)
        {

            Stadium stadium = context.Stadiums.Include("Team").Include("City").FirstOrDefault(s=>s.Id==id);
            if (stadium == null)
            {
                return NotFound();
            }
            StadiumResponseDTO result=new StadiumResponseDTO()
            {
                 StadiumName = stadium.Name,
                 CityName=stadium.City!=null?stadium.City.Name:string.Empty,
                  TeamName=stadium.Team!=null ? stadium.Team.Name:string.Empty
            };
            return Ok(stadium);
        }

        [HttpPost]
        public IActionResult Post([FromBody] StadiumCreateDTO model)

        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }
            else
            {
                Stadium stadium = new Stadium();
                stadium.CityId = (int)model.CityId;
                stadium.Name = model.Name;
                context.Stadiums.Add(stadium);
                context.SaveChanges();
                return Ok(stadium);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StadiumCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Stadium stadium = context.Stadiums.Find(id);
                if (stadium == null)
                {
                    return NotFound();
                }
                stadium.Name = model.Name;
                stadium.CityId = (int)model.CityId;
                context.SaveChanges();

                return Ok(stadium);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Stadium stadium = context.Stadiums.Find(id);
            if (stadium == null) return NotFound();

            context.Stadiums.Remove(stadium);
            context.SaveChanges();
            return Ok(stadium);
        }
    }
}
