using System.ComponentModel.DataAnnotations.Schema;

namespace FootbalTeams.Models.ORM
{
    public class Stadium : BaseEntity
    {
        [ForeignKey("City")]
        public int? CityId { get; set; }
        public virtual City? City { get; set; }

        [ForeignKey("Team")]
        public int? TeamId { get; set; }
        public virtual Team? Team { get; set; }

    }
}
