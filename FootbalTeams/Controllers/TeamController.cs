using FootbalTeams.Contexts;
using FootbalTeams.Models.DTO.PlayerDto;
using FootbalTeams.Models.DTO.TeamDto;
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
            List<TeamResponseDto> results=context.Teams.Include("Players").Include("City").Include("Stadium").Select(t=>
            
            new TeamResponseDto()
            {
                 TeamName=t.Name,
                  AttackPower =t.AttackPower,
                   CityName= t.City!=null?t.City.Name:string.Empty,
                    DefencePower=t.DefencePower,
                     GoalkeepingPower=t.GoalkeepingPower,
                      MiddlefieldPower=t.MiddlefieldPower,
                       StadiumName= t.Stadium != null ? t.Stadium.Name : string.Empty,
                         PlayerNames=t.Players==null?null:
                                     t.Players.Select(p=> new PlayerNameDTO(){ PlayerName= p.Name+" "+ p.Surname}).ToList(),
            }).ToList();

            if (results!=null)
            {
                return Ok(results);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Team team= context.Teams.Include(c => c.City).Include(t=>t.Stadium).Where(t=>t.Id==id).FirstOrDefault();
            if (team == null)
            {
                return NotFound();
            }
            TeamResponseDto result = new TeamResponseDto()
            {
                TeamName = team.Name,
                CityName = team.City == null ? null : team.City.Name,
                StadiumName = team.Stadium == null ? null : team.Stadium.Name,
                PlayerNames = team.Players == null ? null : team.Players.Select(p => new PlayerNameDTO() { PlayerName = p.Name + " " + p.Surname }).ToList(),
                GoalkeepingPower = team.GoalkeepingPower,
                DefencePower = team.DefencePower,
                MiddlefieldPower = team.MiddlefieldPower,
                AttackPower = team.AttackPower,


            };
            return Ok(result);
        }

        [HttpGet("City/{id}")]
        public IActionResult GetTeamsByCityId(int id)
        {
            GetTeamsByCityId result = new GetTeamsByCityId();

            Team team = context.Teams.Include("City").Include("Stadium").Where(t => t.CityId == id).FirstOrDefault();

            if (team == null)
            {
                return NotFound();
            }

            result.TeamName = team.Name;
            result.CityName = team.City.Name;
            result.StadiumName = team.Stadium.Name;
            result.GoalkeepingPower = team.GoalkeepingPower;
            result.DefencePower = team.DefencePower;
            result.MiddlefieldPower = team.MiddlefieldPower;
            result.AttackPower = team.AttackPower;
            result.PlayerNames = team.Players == null ? null : 
                team.Players.Select(p => new PlayerNameDTO() { PlayerName = p.Name + " " + p.Surname }).ToList(); 

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TeamCreateDTO model) {
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
        public IActionResult Put(int id, [FromBody] TeamCreateDTO model ) { 
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
        public IActionResult Delete(int id)
        {
            Team team = context.Teams.Find(id);
            if (team == null) return NotFound();
            return Ok(team);
        }

    }
}
