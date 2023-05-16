namespace FootbalTeams.Models.DTO.PlayerDto
{
    public class PlayerSetPowerDTO
    {
        public int? PlayerPosition { get; set; } = 0;
        public double? GoalkeepingPower { get; set; } = 0;
        public double? DefencePower { get; set; } = 0;
        public double? AttackPower { get; set; } = 0;

    }
}
