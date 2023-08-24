using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Football_Manager
{
    public class Pack
    {
        public int Price { get; set; }
        public int Quantity { get; set; }
        public float ProbabilityFor75 { get; set; }
        public float ProbabilityFor80 { get; set; }
        public float ProbabilityFor85 { get; set; }
        public float ProbabilityFor90 { get; set; }
        public Rarity Rarety { get; set; }

        public Pack(int Price, int Quantity, Rarity Rarety)
        {
            this.Price = Price;
            this.Quantity = Quantity;
            this.Rarety = Rarety;
            switch (Rarety)
            {
                case Rarity.Common:
                    this.ProbabilityFor75 = 20;
                    this.ProbabilityFor80 = 0;
                    this.ProbabilityFor85 = 0;
                    this.ProbabilityFor90 = 0;
                    break;
                case Rarity.Average:
                    this.ProbabilityFor75 = 40;
                    this.ProbabilityFor80 = 10;
                    this.ProbabilityFor85 = 2;
                    this.ProbabilityFor90 = .5f;
                    break;
                case Rarity.Aight:
                    this.ProbabilityFor75 = 50;
                    this.ProbabilityFor80 = 10;
                    this.ProbabilityFor85 = 7;
                    this.ProbabilityFor90 = 1;
                    break;
                case Rarity.Rare:
                    this.ProbabilityFor75 = 100;
                    this.ProbabilityFor80 = 50;
                    this.ProbabilityFor85 = 10;
                    this.ProbabilityFor90 = 4;
                    break;
                case Rarity.VeryRare:
                    this.ProbabilityFor75 = 100;
                    this.ProbabilityFor80 = 100;
                    this.ProbabilityFor85 = 35;
                    this.ProbabilityFor90 = 10;
                    break;
                case Rarity.Legendary:
                    this.ProbabilityFor75 = 100;
                    this.ProbabilityFor80 = 100;
                    this.ProbabilityFor85 = 70;
                    this.ProbabilityFor90 = 35;
                    break;
            }
        }

        public List<Player> OpenPack()
        {
            string[] firstNames = { "Firefox", "Chrome", "Esse", "Eater", "Osborn", "Citro", "Neymar", "Snickers", "Mario", "Aeberhard", "Zivkovic", "Mike", "Benjamin", "Sneaker", "Choc", "Baller", "Opera", "Lessio", "Damjan", "Marin", "Schnitzel", "Enjoyer", "Slayer", "Kayali", "Kaan", "Milk", "Shaker", "Slapper", "Monitor", "GTX", "Fortnite", "Stutz", "Icetea", "Sabbi", "Wallflower", "DiCaprio", "Stuff", "Smelter", "White", "Pinkman", "Rolando", "Silvan", "Velo", "Katz", "Bonuty", "Anes", "Driver", "Controller", "Serkan", "Jurassic", "Schoki", "Obren", "Andrew", "Garfield", "Dielochis" };
            string[] lastNames = firstNames;

            string[] positions = { "GK", "LB", "CB", "CB", "RB", "CDM", "CM", "CAM", "LM", "RM", "LW", "RW", "CF", "ST" };
            List<Player> players = new List<Player>();
            Random random = new Random();

            for (int i = 0; i < Quantity; i++)
            {
                float roll = (float) random.NextDouble() * 100;

                int playerRating = 0;

                if (roll < ProbabilityFor90)
                {
                    playerRating = random.Next(90, 100); // Rating between 90 and 99
                }
                else if (roll < ProbabilityFor85 + ProbabilityFor90)
                {
                    playerRating = random.Next(85, 90); // Rating between 85 and 89
                }
                else if (roll < ProbabilityFor80 + ProbabilityFor85 + ProbabilityFor90)
                {
                    playerRating = random.Next(80, 85); // Rating between 80 and 84
                }
                else if (roll < ProbabilityFor75 + ProbabilityFor80 + ProbabilityFor85 + ProbabilityFor90)
                {
                    playerRating = random.Next(75, 80); // Rating between 75 and 79
                }
                else
                {
                    playerRating = random.Next(58, 75); // Rating between 58 and 74
                }

                // Create a new player with the generated rating and generate name and position
                string genName = firstNames[random.Next(firstNames.Length)] + " " + lastNames[random.Next(lastNames.Length)];
                string genPos = positions[random.Next(positions.Length)];
                int genPrice = new Calc().getPrice(playerRating);
                Player player = new Player(genName, playerRating, 0, genPos, genPrice, random.Next(0, 100) > 95);

                players.Add(player);
            }

            return players;
        }
    }
}
