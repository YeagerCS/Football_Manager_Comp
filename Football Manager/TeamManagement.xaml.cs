using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Football_Manager
{
    /// <summary>
    /// Interaction logic for TeamManagement.xaml
    /// </summary>
    public partial class TeamManagement : Window
    {
        Modell modell = new Modell();
        List<Player> playersStarting;
        List<Player> playersBench;
        Team team = new Team();
        Manager manager = new Manager();

        public TeamManagement()
        {
            InitializeComponent();

            this.ResizeMode = ResizeMode.NoResize;

            if (!File.Exists(new Persistence().GetPath() + "serialization\\players.json"))
            {
                List<Player>[] totalPlayers = GenerateSquad();

                playersStarting = totalPlayers[0];
                playersBench = totalPlayers[1];

                new Persistence().SavePlayersStarting(playersStarting);
                new Persistence().SavePlayersbench(playersBench);

                AddRangeStarting(playersStarting);
                AddRangeBench(playersBench);
            }
            else
            {
                modell.Players = new Persistence().LoadPlayersStarting();
                modell.PlayersBench = new Persistence().LoadPlayersBench();

                playersStarting = modell.Players;
                playersBench = modell.PlayersBench;

                AddRangeStarting(playersStarting);
                AddRangeBench(playersBench);
            }

            if (!File.Exists(new Persistence().GetPath() + "serialization\\team.json"))
            {
                team = new Team();
                team.Name = manager.TeamAssigned.Name;
            }
            else
            {
                team = new Persistence().LoadTeam();
            }

            manager = new Persistence().LoadManagers();
            CalculateTeamRating();
        }

        public void AddRangeStarting(List<Player> list)
        {
            foreach (Player player in list)
            {
                startingLbx.Items.Add(player);
            }
        }

        public void AddRangeBench(List<Player> list)
        {
            foreach(Player player in list)
            {
                benchLbx.Items.Add(player);
            }
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void substituteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(startingLbx.SelectedIndex < 0)
            {
                return;
            }
            int indexStart = startingLbx.SelectedIndex;
            int indexBench = benchLbx.SelectedIndex;

            Player temporaryPlayer = playersStarting[indexStart];
            playersStarting[indexStart] = playersBench[indexBench];
            playersBench[indexBench] = temporaryPlayer;

            startingLbx.Items.Clear();
            benchLbx.Items.Clear();

            AddRangeStarting(playersStarting);
            AddRangeBench(playersBench);
            new Persistence().SavePlayersStarting(playersStarting);
            new Persistence().SavePlayersbench(playersBench);
            CalculateTeamRating();
        }

        public List<Player>[] GenerateSquad()
        {
            string[] firstNames = { "Firefox", "Chrome", "Esse", "Eater", "Osborn", "Citro", "Neymar", "Snickers", "Mario", "Aeberhard", "Zivkovic", "Mike", "Benjamin", "Sneaker", "Choc", "Baller", "Opera", "Lessio", "Damjan", "Marin", "Schnitzel", "Enjoyer", "Slayer", "Kayali", "Kaan", "Milk", "Shaker", "Slapper", "Monitor", "GTX", "Fortnite", "Stutz", "Icetea", "Sabbi", "Wallflower", "DiCaprio", "Stuff", "Smelter", "White", "Pinkman", "Rolando", "Silvan", "Velo", "Katz", "Bonuty", "Anes", "Driver", "Controller", "Serkan", "Jurassic", "Schoki", "Obren", "Andrew", "Garfield", "Dielochis" };
            string[] lastNames = firstNames;

            string[] positions = { "GK", "LB", "CB", "CB", "RB", "CDM", "CM", "CAM", "LM", "RM", "LW", "RW", "CF", "ST" };

            Random rand = new Random();

            List<Player> players = new List<Player>();
            List<Player> substitutes = new List<Player>();

            //Goalkeeper
            players.Add(new Player(firstNames[rand.Next(firstNames.Length)] + " " + lastNames[rand.Next(lastNames.Length)], rand.Next(65, 79), 1, "GK", 3500 + (rand.Next(65, 79) - 65) * 100, rand.Next(8) == 0));

            // Defenders
            for (int i = 1; i <= 4; i++)
            {
                string name = firstNames[rand.Next(firstNames.Length)] + " " + lastNames[rand.Next(lastNames.Length)];

                int rating = rand.Next(65, 79);

                int shirtNumber = rand.Next(1, 41);
                while (players.Exists(p => p.ShirtNumber == shirtNumber))
                {
                    shirtNumber = rand.Next(1, 41);
                }

                //Select next Position 0 - 4th index in the array
                string position = positions[i];

                // 65 Rated = 3500CQ; Calculation for price.
                int price = new Calc().getPrice(rating);

                players.Add(new Player(name, rating, shirtNumber, position, price, rand.Next(8) == 0));
            }

            // Midfielders
            for (int i = 5; i <= 7; i++)
            {
                string name = firstNames[rand.Next(firstNames.Length)] + " " + lastNames[rand.Next(lastNames.Length)];

                int rating = rand.Next(65, 79);

                int shirtNumber = rand.Next(1, 41);
                while (players.Exists(p => p.ShirtNumber == shirtNumber))
                {
                    shirtNumber = rand.Next(1, 41);
                }

                // Select next Position 5 - 7th index in the array => for(int i = 5; i <= 7; ...)
                string position = positions[i];

                int price = new Calc().getPrice(rating);

                players.Add(new Player(name, rating, shirtNumber, position, price, rand.Next(8) == 0));
            }
            // Strikers
            for (int i = 8; i <= 10; i++)
            {
                string name = firstNames[rand.Next(firstNames.Length)] + " " + lastNames[rand.Next(lastNames.Length)];

                int rating = rand.Next(65, 79);

                int shirtNumber = rand.Next(1, 41);
                while (players.Exists(p => p.ShirtNumber == shirtNumber))
                {
                    shirtNumber = rand.Next(1, 41);
                }

                // 8 - 10th Index in the array => for(int i = 8; i <= 10; ...)
                string position = positions[i];

                int price = new Calc().getPrice(rating);

                players.Add(new Player(name, rating, shirtNumber, position, price, rand.Next(8) == 0));
            }

            //Bench Players
            for (int i = 0; i <= 3; i++)
            {
                string name = firstNames[rand.Next(firstNames.Length)] + " " + lastNames[rand.Next(lastNames.Length)];

                int rating = rand.Next(65, 79);

                int shirtNumber = rand.Next(1, 41);
                while(players.Exists(p => p.ShirtNumber == shirtNumber) || substitutes.Exists(p => p.ShirtNumber == shirtNumber))
                {
                    shirtNumber = rand.Next(1, 41);
                }

                // Completely random positions
                string position = positions[rand.Next(positions.Length)];

                int price = new Calc().getPrice(rating);

                substitutes.Add(new Player(name, rating, shirtNumber, position, price, rand.Next(8) == 0));
            }

            List<Player>[] listList = { players, substitutes };
            return listList;
        }

        public void CalculateTeamRating()
        {
            int sum = 0;
            foreach(Player player in playersStarting)
            {
                sum += player.Rating;
            }

            int count = playersStarting.Count;
            if(count != 0)
            {
                int rating = sum / count;
                team.Rating = rating;
                ratingLbl.Content = team.Rating + "";
                manager.TeamAssigned = team;
                new Persistence().SaveManagers(manager);
                new Persistence().SaveTeam(team);
            }
            
        }

        private void sellBtn_Click(object sender, RoutedEventArgs e)
        {
            if(startingLbx.SelectedIndex != -1 && benchLbx.SelectedIndex != -1)
            {
                MessageBox.Show("Please Select one player only.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                startingLbx.SelectedIndex = -1;
                benchLbx.SelectedIndex = -1;
            }
            else if(startingLbx.SelectedIndex != -1 && benchLbx.SelectedIndex == -1)
            {
                MessageBox.Show("Please put the player to be sold on the bench.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                startingLbx.SelectedIndex = -1;
                benchLbx.SelectedIndex = -1;
            }
            else if(benchLbx.SelectedIndex != -1 && startingLbx.SelectedIndex == -1)
            {
                MessageBoxResult result = MessageBox.Show($"Sell {playersBench[benchLbx.SelectedIndex].Name} for {playersBench[benchLbx.SelectedIndex].Price - new Calc().GeneratePriceRemoval(playersBench[benchLbx.SelectedIndex].Rating)}.", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    manager.AddMoney(playersBench[benchLbx.SelectedIndex].Price - new Calc().GeneratePriceRemoval(playersBench[benchLbx.SelectedIndex].Rating));
                    playersBench.RemoveAt(benchLbx.SelectedIndex);

                    benchLbx.Items.Clear();
                    AddRangeBench(playersBench);
                    new Persistence().SaveManagers(manager);
                    new Persistence().SavePlayersbench(playersBench);

                }
            }
        }

        private void startingLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(startingLbx.SelectedIndex != -1)
            {
                sellPrice.Content = playersStarting[startingLbx.SelectedIndex].Price - new Calc().GeneratePriceRemoval(playersStarting[startingLbx.SelectedIndex].Rating) + "CQ";
            }
        }

        private void benchLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(benchLbx.SelectedIndex != -1)
            {
                sellPrice.Content = playersBench[benchLbx.SelectedIndex].Price - new Calc().GeneratePriceRemoval(playersBench[benchLbx.SelectedIndex].Rating) + "CQ";
            }
        }
    }
}
