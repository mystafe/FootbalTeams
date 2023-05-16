namespace FootbalTeams.Models.DTO.CountryDto
{
    public class CountryResponseDto
    {
        public string CountryName { get; set; }
        public List<CityNameDto>? CityNames { get; set; }
    }

  
}
