using FootbalTeams.Models.ORM;

namespace FootbalTeams.Models.DTO.PlayerDto
{
    public class PlayerResponseDto
    {
        public string PlayerName { get; set; }
        public string? PlayerSurname { get; set; }
        public PlayerPositions? PlayerPosition { get; set; }
        public double? GoalkeepingPower { get; set; }
        public double? DefencePower { get; set; }
        public double? AttackPower { get; set; }
        public string? TeamName { get; set; }
        public string? CityName { get; set; }
    }
}
