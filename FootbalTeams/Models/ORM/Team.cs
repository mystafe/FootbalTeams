using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootbalTeams.Models.ORM
{
    public class Team:BaseEntity
    {

        public double? GoalkeepingPower { get; set; } = 0;
        public double? DefencePower { get; set; } = 0;
        public double? MiddlefieldPower { get; set; } = 0;
        public double? AttackPower { get; set; } = 0;

        public virtual ICollection<Player>? Players { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }
        public virtual City? City { get; set; }

        [ForeignKey("Stadium")]
        public int? StadiumId { get; set; }
        public virtual Stadium? Stadium { get; set; }
    }
}
