using FootbalTeams.Contexts;
using FootbalTeams.Models.DTO;
using FootbalTeams.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FootbalTeams.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class PlayerController : ControllerBase
    {
        private readonly FootbalContext context;

        public PlayerController(FootbalContext _context)
        {
            context = _context;
        }


        [HttpGet]
        public IActionResult  Get()
        {
            List<Player> players=context.Players.AsQueryable().ToList();
            if (players.Count > 0)
            {
                return Ok(players);

            }
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Player player = context.Players.Find(id);
            if (player == null)
            {
                return NoContent();
            }
            else
            {
                return Ok(player);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] PlayerCreateDTO model)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            else
            {
                Player player=new Player() 
                {
                    TeamId = model.TeamId , 
                    DefencePower=model.DefencePower,
                     AttackPower=model.AttackPower,
                      GoalkeepingPower=model.GoalkeepingPower,
                       PlayerPosition = model.PlayerPosition,
                        Name = model.Name,
                        Surname = model.Surname,
                         CityId = (int)model.CityId,
                          
                };
                context.Players.Add(player);
                try
                {
                    context.SaveChanges();
                    return Ok(player);
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PlayerCreateDTO model)
        {
            Player player=context.Players.Find(id);
            if (!ModelState.IsValid)
            {
                    return BadRequest(ModelState);
            }
            else if(player==null)
            {
                return NotFound(model);
            }
            else if (model==null)
            {
                 return BadRequest(model);
                
            }
            else
            {
                player.Name = model.Name;
                player.Surname = model.Surname;
              
                player.CityId = (int)model.CityId;

                player.PlayerPosition= model.PlayerPosition;
                player.AttackPower = model.AttackPower;
                player.DefencePower = model.DefencePower;
                player.GoalkeepingPower = model.GoalkeepingPower;
                player.TeamId= model.TeamId;
                context.SaveChanges();
                return Ok(player);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Player player = context.Players.Find(id);
            if (player==null) {
                return NotFound();
            }
            else
            {
                context.Players.Remove(player);
                context.SaveChanges();
                return Ok(player);
            }
        }
    }
}
