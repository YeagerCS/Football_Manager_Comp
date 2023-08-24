using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.DirectoryServices;
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
using System.Windows.Threading;
using System.Xml.Linq;

namespace Football_Manager
{
    /// <summary>
    /// Interaction logic for GameLoop.xaml
    /// </summary>
    public partial class GameLoop : Window
    {
        int counter = 0;
        Modell modell = new Modell();
        Manager manager = new Manager();
        Team team = new Team();
        Team enemyTeam = new Team();
        Persistence serv = new Persistence();
        public GameLoop()
        {
            InitializeComponent();

            this.ResizeMode = ResizeMode.NoResize;
            team = serv.LoadTeam();
            manager = serv.LoadManagers();
            modell.Players = serv.LoadPlayersStarting();
            modell.PlayersBench = serv.LoadPlayersBench();

            enemyTeam = RandomizeEnemyTeam();
            team1Lbl.Content = team.Name;
            team2Lbl.Content = enemyTeam.Name;
            ratingLbl.Content = team.Rating;
            ratingEnemyLbl.Content = enemyTeam.Rating;
            backBtn.IsEnabled = false;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2.5);
            timer.Tick += Timer_Tick;
            timer.Start();

            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(.8);
            timer2.Tick += Timer2_Tick;
            timer2.Start();
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            simulationLbl.Content += ".";
            counter++;
            if(counter == 3)
            {
                timer.Stop();
                Simulation(team.Name, team.Rating, enemyTeam);
                backBtn.IsEnabled = true;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            Grid2.Visibility = Visibility.Hidden;
            timer.Stop();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        public Team RandomizeEnemyTeam()
        {
            Random random = new Random();
            int index = random.Next(modell.EntityTeams.Count);
            Team eTeam = modell.EntityTeams[index];
            return eTeam;
        }

        // Simulates the game, while considering the team ratings. With a 99 rated team, you're almost certain to win every match
        public void Simulation(String teamName, double rating, Team eTeam)
        {
            string eName = eTeam.Name;
            double eRating = eTeam.Rating;

           

            team1Lbl2.Content = teamName;
            team2Lbl2.Content = eName;
            double ratingDiff = eRating - rating;
            int winnerDetection = new Random().Next(100);
            if(winnerDetection > (50 + ratingDiff)) // Winner chosen by random chance, higher rated team has higher chance
            {
                //Your team wins
                int goals = new Random().Next(6);
                int eGoals = new Random().Next(4);
                while(goals <= eGoals)
                {
                    goals = new Random().Next(6);
                    eGoals = new Random().Next(4);
                }

                //goals : eGoals
                team1Goals.Content = goals;
                team2Goals.Content = eGoals;
                PlayerGoals(goals);

                winLoseDraw.Content = "You Win!";
                team.addWins();
                manager.AddMoney(750);
                manager.Rating = Math.Round(manager.Rating * 1.015, 2);

                serv.SaveManagers(manager);
                serv.SavePlayersStarting(modell.Players);
                serv.SaveTeam(team);

                modell.matches.Add($"{teamName} {goals} : {eGoals} {eName}");
                serv.SaveMatches(modell.matches);
            }
            else
            {
                //Enemy won, but chance for draw
                int goals = new Random().Next(4);
                int eGoals = new Random().Next(6);
                while(eGoals < goals)
                {
                    goals = new Random().Next(4);
                    eGoals = new Random().Next(6);
                }

                if(goals == eGoals)
                {
                    //Draw

                    //goals : eGoals
                    team1Goals.Content = goals;
                    team2Goals.Content = eGoals;
                    PlayerGoals(goals);

                    winLoseDraw.Content = "Draw!";

                    team.addDraws();
                    manager.AddMoney(300);
                    manager.Rating = Math.Round(manager.Rating * 1, 2);
                    modell.matches.Add($"{teamName} {goals} : {eGoals} {eName}");
                    serv.SaveMatches(modell.matches);
                }
                else
                {
                    //Enemy Win
 
                    //goals : eGoals
                    team1Goals.Content = goals;
                    team2Goals.Content = eGoals;
                    PlayerGoals(goals);

                    winLoseDraw.Content = "You lose!";
                    team.addLoses();
                    manager.AddMoney(120);
                    manager.Rating = Math.Round(manager.Rating * .985, 2);
                    modell.matches.Add($"{teamName} {goals} : {eGoals} {eName}");
                    serv.SaveMatches(modell.matches);
                }

                serv.SaveManagers(manager);
                serv.SavePlayersStarting(modell.Players);
                serv.SaveTeam(team);
            }
        }

        public void PlayerGoals(int goals)
        {
            List<Player> players = new List<Player>();
            int[] minutes = new int[goals];
            for (int i = 0; i < goals; i++)
            {
                Player playerSelected = modell.Players[new Random().Next(modell.Players.Count)];
                minutes[i] = new Random().Next(1, 91);
                playerSelected.Goals++;
                players.Add(playerSelected);
            }

            List<string> DisplayStrings = new List<string>();
            QuickSort(minutes, 0, minutes.Length - 1);
            for(int i = 0; i < goals; i++)
            {
                DisplayStrings.Add(minutes[i] + "' " + players[i].Name);
            }

            foreach(string elem in DisplayStrings)
            {
                goalsLbx.Items.Add(elem);
            }

        }
        public Player SelectRandomPlayer(List<Player> players)
        {
            List<Player> placeHolder = new List<Player>();
            foreach(Player player in players)
            {
                placeHolder.Add(player);
            }
            while (true)
            {
                int rand = new Random().Next(0, 100);
                for (int i = 0; i < players.Count; i++)
                {
                    if (rand < players[i].Rating)
                    {
                        return players[i];
                    }
                }
            }

            return null;
        }

        public static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(array, left, right);
                QuickSort(array, left, pivotIndex - 1);
                QuickSort(array, pivotIndex + 1, right);
            }
        }

        private static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    Swap(array, i, j);
                }
            }

            Swap(array, i + 1, right);
            return i + 1;
        }

        private static void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
