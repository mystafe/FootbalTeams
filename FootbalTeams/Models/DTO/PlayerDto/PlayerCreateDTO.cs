using FootbalTeams.Models.ORM;

namespace FootbalTeams.Models.DTO.PlayerDto
{
    public class PlayerCreateDTO
    {
        public string Name { get; set; } = "Player";
        public string? Surname { get; set; }
        public PlayerPositions PlayerPosition { get; set; }
        public double? GoalkeepingPower { get; set; } = 0;
        public double? DefencePower { get; set; } = 0;
        public double? AttackPower { get; set; } = 0;
        public int? TeamId { get; set; }
        public int? CityId { get; set; }
    }
}
