using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Football_Manager
{
    internal class Modell
    {
        public List<Player> Players { get; set; }
        public List<Player> PlayersBench { get; set; }
        public List<Team> EntityTeams { get; set; }
        public List<string> matches { get; set; }
        public Modell()
        {
            Players = new List<Player>();
            PlayersBench = new List<Player>();
            if (!File.Exists(new Persistence().GetPath() + "serialization\\entity.json"))
            {
                EntityTeams = new List<Team>()
                {
                    new Team("Coke Runners", 59, 1.005),
                    new Team("Java Smelters", 62, 1.005),
                    new Team("Task Managers", 65, 1.01),
                    new Team("Ciscos", 67, 1.005),
                    new Team("Shahmir Readers", 72, 1.003),
                    new Team("Chlorkohle", 74, 1.001),
                    new Team("Alpha Ballers", 77, 1.001),
                    new Team("Jeremier's", 77, 1.001),
                    new Team("Linear Functions", 79, 1.001),
                    new Team("Propansmelters", 80, 1),
                    new Team("Equation Fallers", 82, 1.001),
                    new Team("Supersnackers", 83, 1.0005),
                    new Team("Monitor Slappers", 57, 1.01),
                    new Team("Nissan Eaters", 84, 1.005),
                    new Team("Fudgetive Paints", 86, 1.005),
                    new Team("Toyota Rappers", 89, 1.001),
                    new Team("Microsoft Paints", 92, 1.001),
                    new Team("Spongebobbers", 61, 1.005),
                    new Team("Maze Banks", 65, 1.005),
                    new Team("Tangents", 66, 1.001)
                };

                new Persistence().SaveEntityTeams(EntityTeams);
            }
            else
            {
                EntityTeams = new Persistence().LoadEntityTeams();
            }
            matches = new Persistence().LoadMatches();
        }

        
    }
}
