using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Football_Manager
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {
        Manager manager = new Manager();
        Team team = new Team();
        Modell modell = new Modell();
        List<Player> list = new List<Player>();
        public Stats()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            manager = new Persistence().LoadManagers();
            team = new Persistence().LoadTeam();
            modell.Players = new Persistence().LoadPlayersStarting();
            modell.PlayersBench = new Persistence().LoadPlayersBench();
            grid2.Visibility = Visibility.Hidden;
            Init();
        }


        public void Init()
        {
            AddRange(modell.Players, modell.PlayersBench);
            ratingLbl.Content = team.Rating;
            matchesPlayed.Content = team.Wins + team.Draws + team.Loses;
            wins.Content = team.Wins;
            draws.Content = team.Draws;
            losses.Content = team.Loses;

            int goals = 0;
            foreach(Player player in modell.Players)
            {
                goals += player.Goals;
            }

            foreach(Player player in modell.PlayersBench)
            {
                goals += player.Goals;
            }

            goalsLbl.Content = "" + goals;

            coinLbl.Content = manager.Money + "CQ";
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        public void AddRange(List<Player> list1, List<Player> list2)
        {
            list.Clear();
            startingLbx.Items.Clear();
            foreach(Player player in list1)
            {
                list.Add(player);
            }

            foreach(Player player in list2)
            {
                list.Add(player);
            }

            foreach(Player player in list)
            {
                startingLbx.Items.Add(player);
            }
        }

        private void allPlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Visible;
        }

        private void backBtn2_Click(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Hidden;
        }

        private void startingLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Player player = list[startingLbx.SelectedIndex];
            playerPositionLbl.Content = player.Position;
            playerShirtnrLbl.Content = player.ShirtNumber;
            playerGoalsLbl.Content = player.Goals;
            if (player.Multiupgrade)
            {

                playerRatingLbl.Content = "Ω " + player.Rating;
            }
            else
            {
                playerRatingLbl.Content = player.Rating;
            }
        }
    }
}
