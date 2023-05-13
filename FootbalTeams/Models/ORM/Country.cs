namespace FootbalTeams.Models.ORM
{
    public class Country:BaseEntity
    {
        public virtual ICollection<City>? Cities { get; set; }
    }
}
