using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootbalTeams.Models.ORM
{
    public class Team:BaseEntity
    {

        public Team()
        {
            //SetPower();

        }

        public double? GoalkeepingPower { get; set; } = 0;
        public double? DefencePower { get; set; } = 0;
        public double? MiddlefieldPower { get; set; } = 0;
        public double? AttackPower { get; set; } = 0;


        [ForeignKey("City")]
        public int? CityId { get; set; }
        public virtual City? City { get; set; }

        [ForeignKey("Stadium")]
        public int? StadiumId { get; set; }
        public virtual Stadium? Stadium { get; set; }
        public virtual List<Player>? Players { get; set; }

        public void SetPower()
        {

            if (Players != null)
            {
                int count1=0,count2=0,count3=0,count4 = 0;
                GoalkeepingPower = 0;DefencePower = 0;MiddlefieldPower = 0; AttackPower=0;
                Players.ForEach(p => {

                    if (p.PlayerPosition == PlayerPositions.GoalKeeper)
                    {
                        count1++;
                        GoalkeepingPower += (p.GoalkeepingPower);
                    }
                    else if (p.PlayerPosition == PlayerPositions.Defencer)
                    {
                        count2++;
                        DefencePower += (p.DefencePower);
                    }
                    else if (p.PlayerPosition == PlayerPositions.Middlefielder)
                    {
                        count3++;
                        MiddlefieldPower += (p.DefencePower * 0.4 + p.AttackPower * 0.6);
                        AttackPower += (p.AttackPower * 0.4);
                        DefencePower += (p.DefencePower * 0.4);
                    }
                    else if (p.PlayerPosition == PlayerPositions.Attacker)
                    {
                        count4++;
                        AttackPower += (p.AttackPower);
                    }

                });

                GoalkeepingPower = count1 > 0 ? Math.Round((double)(GoalkeepingPower   / count1),                2) : 0;
                DefencePower     = count2 > 0 ? Math.Round((double)(DefencePower      / (count2+(count3*0.4))),  2) : 0;
                MiddlefieldPower = count3 > 0 ? Math.Round((double)(MiddlefieldPower  / count3),                 2) : 0;
                AttackPower      = count4 > 0 ? Math.Round((double)(AttackPower       / (count4+(count3*0.4))),  2) : 0;
            }

        }

    }
}
