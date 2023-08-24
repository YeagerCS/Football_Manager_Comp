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
using System.Diagnostics;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace Football_Manager
{
    /// <summary>
    /// Interaction logic for DebuggingWindow.xaml
    /// </summary>
    public partial class DebuggingWindow : Window
    {
        Modell modell = new Modell();
        Persistence serv = new Persistence();
        Manager manager = new Manager();
        Team team = new Team();
        List<Player> list = new List<Player>();
        public DebuggingWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            modell.Players = serv.LoadPlayersStarting();
            modell.PlayersBench = serv.LoadPlayersBench();

            manager = serv.LoadManagers();
            team = serv.LoadTeam();
            coinLbl.Content = manager.Money+ "CQ";
            grid2.Visibility = Visibility.Hidden;
            grid3.Visibility = Visibility.Hidden;
            AddRange(modell.Players, modell.PlayersBench);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            int result;
            if (!string.IsNullOrWhiteSpace(amountTbx.Text) && int.TryParse(amountTbx.Text, out result))
            {
                manager.AddMoney(Convert.ToInt32(amountTbx.Text));
                serv.SaveManagers(manager);
                coinLbl.Content = manager.Money + "CQ";
            }
            else
            {
                MessageBox.Show("The amount you entered is either empty or not an Integer", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void resetStateBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Reset entire gamestate?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(message == MessageBoxResult.Yes)
            {
                string dirPath = serv.GetPath() + "serialization";
                var files = Directory.EnumerateFiles(dirPath);

                foreach(string file in files)
                {
                    File.Delete(file);
                }
                Directory.Delete(dirPath);
                File.Delete(serv.GetPathTxtPath());

                MessageBox.Show("The Game state has reset. A restart is required.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Process process = Process.GetCurrentProcess();

                Process.Start(process.MainModule.FileName);

                Application.Current.Shutdown();
            }
        }

        private void resetShopBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Reset shop?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (message == MessageBoxResult.Yes)
            {
                File.Delete(serv.GetPath() + "serialization\\shop.json");
            }

            MessageBox.Show("The Shop is reset.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        private void resetTeamBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Reset team?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (message == MessageBoxResult.Yes)
            {
                File.Delete(serv.GetPath() + "serialization\\players.json");
                File.Delete(serv.GetPath() + "serialization\\bench.json");
            }

            MessageBox.Show("The team is reset.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void maxOutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult message = MessageBox.Show("Upgrade all ratings to 99?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (message == MessageBoxResult.Yes)
            {
                List<Player> player = serv.LoadPlayersStarting();
                List<Player> player2 = serv.LoadPlayersBench();

                foreach(Player p in player)
                {
                    p.DisplayString = p.DisplayString.Replace(p.Rating + "", "99");
                    p.Rating = 99;
                    p.Price = new Calc().getPrice(p.Rating);
                }

                foreach (Player p in player2)
                {
                    p.DisplayString = p.DisplayString.Replace(p.Rating + "", "99");
                    p.Rating = 99;
                    p.Price = new Calc().getPrice(p.Rating);
                }

                serv.SavePlayersStarting(player);
                serv.SavePlayersbench(player2);
            }
            MessageBox.Show("The team is upgraded.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Visible;

        }

        public void AddRange(List<Player> players, List<Player> playersBench)
        {
            foreach(Player player in players)
            {
                startingLbx.Items.Add(player);
                list.Add(player);
            }

            foreach(Player player in playersBench)
            {
                startingLbx.Items.Add(player);
                list.Add(player);
            }
        }

        private void editBtn2_Click(object sender, RoutedEventArgs e)
        {
            grid3.Visibility = Visibility.Visible;
            teamNameTbx.Text = team.Name;
            multiplierTbx.Text = team.Multiplier + "";
        }

        private void startingLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(startingLbx.SelectedIndex != -1)
            {
                nameTbx.Text = list[startingLbx.SelectedIndex].Name;
                positionTbx.Text = list[startingLbx.SelectedIndex].Position;
                ratingTbx.Text = list[startingLbx.SelectedIndex].Rating + "";
                shirtNrTbx.Text = list[startingLbx.SelectedIndex].ShirtNumber + "";
            }
        }

        private void backBtn2_Click(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Hidden;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(startingLbx.SelectedIndex != -1)
            {
                Player previousValues = list[startingLbx.SelectedIndex];
                string name = nameTbx.Text;
                string position = positionTbx.Text;
                int rating = Convert.ToInt32(ratingTbx.Text);
                int shirtNumber = Convert.ToInt32(shirtNrTbx.Text);

                int index = -1;
                if(modell.Players.IndexOf(previousValues) == -1)
                {
                    index = modell.PlayersBench.IndexOf(previousValues);
                    modell.PlayersBench[index].Name = name;
                    modell.PlayersBench[index].Position = position;
                    modell.PlayersBench[index].Rating = rating;
                    modell.PlayersBench[index].ShirtNumber = shirtNumber;
                    if (modell.PlayersBench[index].Multiupgrade == true)
                    {
                        modell.PlayersBench[index].DisplayString = shirtNumber + " " + name + " " + rating + " " + position + " Ω";
                    }
                    else
                    {
                        modell.PlayersBench[index].DisplayString = shirtNumber + " " + name + " " + rating + " " + position;
                    }
                    serv.SavePlayersbench(modell.PlayersBench);
                }
                else
                {
                    index = modell.Players.IndexOf(previousValues);
                    modell.Players[index].Name = name;
                    modell.Players[index].Position = position;
                    modell.Players[index].Rating = rating;
                    modell.Players[index].ShirtNumber = shirtNumber;
                    if (modell.Players[index].Multiupgrade == true)
                    {
                        modell.Players[index].DisplayString = shirtNumber + " " + name + " " + rating + " " + position + " Ω";
                    }
                    else
                    {
                        modell.Players[index].DisplayString = shirtNumber + " " + name + " " + rating + " " + position;
                    }
                    serv.SavePlayersStarting(modell.Players);
                }

                MessageBox.Show("Successfully Updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                startingLbx.Items.Clear();
                AddRange(modell.Players, modell.PlayersBench);
                nameTbx.Text = "";
                positionTbx.Text = "";
                ratingTbx.Text = "";
                shirtNrTbx.Text = "";
            }
        }

        private void backBtn3_Click(object sender, RoutedEventArgs e)
        {
            grid3.Visibility = Visibility.Hidden;
        }

        private void saveBtn2_Click(object sender, RoutedEventArgs e)
        {
            string name = teamNameTbx.Text;
            double mulitplier = Convert.ToInt32(multiplierTbx.Text);

            team.Name = name;
            team.Multiplier = mulitplier;
            serv.SaveTeam(team);
            MessageBox.Show("Successfully Updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
