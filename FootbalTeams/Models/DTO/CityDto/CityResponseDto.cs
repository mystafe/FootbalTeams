using FootbalTeams.Models.DTO.TeamDto;

namespace FootbalTeams.Models.DTO.CityDto
{
    public class CityResponseDto
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public List<TeamNameDto>? TeamNames { get; set; }
    }
}
