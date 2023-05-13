using FootbalTeams.Models.ORM;

namespace FootbalTeams.Models.DTO
{
    public class TeamCreateDTO
    {
        public string Name { get; set; } = "Team";
        public int? CityId { get; set; }

        public int? StadiumId { get; set; }
    }
}
