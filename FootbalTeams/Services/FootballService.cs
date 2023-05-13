using FootbalTeams.Models.ORM;

namespace FootbalTeams.Services
{
    public class FootballService
    {

        Team team = new Team()
        {
            Name = "Test Team",
        };
        Player player = new Player() { 
            DefencePower=50,
            AttackPower=70,
            GoalkeepingPower=30
        };

        
    }
}
