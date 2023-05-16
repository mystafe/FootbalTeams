using FootbalTeams.Contexts;
using FootbalTeams.Models.DTO.PlayerDto;
using FootbalTeams.Models.ORM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Xml;

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
        public IActionResult Get()
        {
            List<PlayerResponseDto> players = context.Players.Include("Team").Include("City").AsQueryable().Select(t => new PlayerResponseDto()
            {
                PlayerName = t.Name,
                PlayerSurname = t.Surname,
                PlayerPosition = t.PlayerPosition,
                AttackPower = t.AttackPower,
                DefencePower = t.DefencePower,

                CityName = t.City!=null?t.City.Name:string.Empty,
                GoalkeepingPower = t.GoalkeepingPower,
                TeamName = t.Team!=null?t.Team.Name:string.Empty,
               

            }).ToList();
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
            Player player = context.Players.Include(p=>p.City).Include(p=>p.Team).AsQueryable().FirstOrDefault(p=>p.Id==id);
            if (player == null)
            {
                return NoContent();
            }
            else
            {
                PlayerResponseDto result = new PlayerResponseDto()
                {
                    PlayerName = player.Name,
                    PlayerSurname = player.Surname,
                    CityName = player.City != null ? player.City.Name : String.Empty,
                    TeamName = player.Team != null ? player.Team.Name : String.Empty,
                    PlayerPosition = player.PlayerPosition,
                    GoalkeepingPower = player.GoalkeepingPower,
                    DefencePower = player.DefencePower,
                    AttackPower = player.AttackPower,
                };
                return Ok(result);
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
                   Team team = context.Teams.Include("Players").Where(t => t.Id == player.TeamId).FirstOrDefault();
                   List<Player> player1=new List<Player>();
                   team.SetPower();
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
                Team team = context.Teams.Find(player.TeamId);
                team.SetPower();
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

        [HttpPost("set/{id}")]
        public IActionResult SetPower(int id, [FromBody] PlayerCreateDTO model)
        {
            Player player = context.Players.Find(id);
            if (player == null)
            {
                return NotFound();
            }
            else if (!ModelState.IsValid) return BadRequest();
            else
            {
                player.GoalkeepingPower = model.GoalkeepingPower;
                player.DefencePower=model.DefencePower;
                player.AttackPower = model.AttackPower;
                Team team=context.Teams.Find(player.TeamId);
   
                team.SetPower();
                context.SaveChanges();
                PlayerResponseDto result=new PlayerResponseDto() { 
                    PlayerName=player.Name,
                     PlayerSurname=player.Surname,
                      CityName=player.City==null?String.Empty:player.City.Name,
                       TeamName=team.Name,
                        PlayerPosition=player.PlayerPosition,
                         GoalkeepingPower=player.GoalkeepingPower,
                          DefencePower = player.DefencePower,
                           AttackPower=player.AttackPower,
                       
                };
                return Ok(result);
            }

        }
    }
}
