using FootbalTeams.Contexts;
using FootbalTeams.Models.DTO;
using FootbalTeams.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootbalTeams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly FootbalContext context;
        public TeamController(FootbalContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            List<Team> teams= context.Teams.AsQueryable().ToList();

            if (teams.Count > 0) {
            return Ok(teams);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            Team team= context.Teams.Find(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost]
        public ActionResult Post([FromBody] TeamCreateDTO model) {
            if (model == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            Team team = new Team()
            {
                Name = model.Name,
                CityId = (int)model.CityId,
                StadiumId = (int)model.StadiumId,
            };
            context.Teams.Add(team);
            context.SaveChanges();
            return Ok(team);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TeamCreateDTO model ) { 
            if (model == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest(model);
            Team team= context.Teams.Find(id);
            if (team == null) return NotFound();
            team.Name = model.Name;
            team.StadiumId= (int)model.StadiumId;
            team.CityId= (int)model.CityId;
            context.SaveChanges();
            return Ok(team);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Team team = context.Teams.Find(id);
            if (team == null) return NotFound();
            return Ok(team);
        }

    }
}
