using System.ComponentModel.DataAnnotations.Schema;

namespace FootbalTeams.Models.ORM
{
    public class City : BaseEntity
    {
        [ForeignKey("Country")]
        public int? CountryId { get; set; }
        public Country? Country { get; set; }
        public virtual ICollection<Team>? Teams { get; set; }
        public virtual ICollection<Player>? Players { get; set; }
        public virtual ICollection<Stadium>? Stadiums { get; set; }

    }
}
