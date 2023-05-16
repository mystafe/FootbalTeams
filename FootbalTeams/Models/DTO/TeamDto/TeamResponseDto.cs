using FootbalTeams.Models.DTO.PlayerDto;
using FootbalTeams.Models.ORM;

namespace FootbalTeams.Models.DTO.TeamDto
{
    public class TeamResponseDto
    {
        public string TeamName { get; set; } 
        public string? CityName { get; set; }
        public string? StadiumName { get; set; }
        public double? GoalkeepingPower { get; set; }
        public double? DefencePower { get; set; }
        public double? MiddlefieldPower { get; set; }
        public double? AttackPower { get; set; }
        public List<PlayerNameDTO>? PlayerNames { get; set; }

    }
}

