using System.ComponentModel.DataAnnotations.Schema;

namespace FootbalTeams.Models.ORM
{
    public enum PlayerPositions
    {
        GoalKeeper=1,
        Defencer,
        Middlefielder,
        Attacker

    };
    public class Player:BaseEntity
    {
        public string? Surname { get; set; }
        public PlayerPositions? PlayerPosition { get; set; }
        public double? GoalkeepingPower { get; set; } = 0;
        public double? DefencePower { get; set; } = 0;
        public double? AttackPower { get; set; } = 0;

        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        public virtual Team? Team { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }
        public virtual City? City { get; set; }


    }
}
