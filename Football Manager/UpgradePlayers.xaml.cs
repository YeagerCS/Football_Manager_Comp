using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Football_Manager
{
    /// <summary>
    /// Interaction logic for UpgradePlayers.xaml
    /// </summary>
    public partial class UpgradePlayers : Window
    {
        Modell modell = new Modell();
        Manager manager = new Manager();

        int[] prices = { 3500, 6800, 17000, 36000 };
        int[] ratings = { 1, 2, 5, 10 };
        public UpgradePlayers()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;

            if (!File.Exists(new Persistence().GetPath() + "serialization\\players.json"))
            {
                MessageBox.Show("Seems like you didn't Generate your team yet. Click on 'Team Management' in the menu to do so", "Something is missing", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
            }

            modell.Players = new Persistence().LoadPlayersStarting();
            modell.PlayersBench = new Persistence().LoadPlayersBench();

            manager = new Persistence().LoadManagers();
            coinAmount.Content = manager.Money + "CQ";
            AddRangeStarting(modell.Players);
            AddRangeBench(modell.PlayersBench);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
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
            foreach (Player player in list)
            {
                benchLbx.Items.Add(player);
            }
        }

        public void AddRange(List<Player> list, ListBox listbox)
        {
            foreach(Player player in list)
            {
                listbox.Items.Add(player);
            }
        }

        private void upgrade1Btn_Click(object sender, RoutedEventArgs e)
        {
            if(startingLbx.SelectedIndex != -1)
            {
                Purchase(startingLbx, modell.Players, 0);
            }

            if(benchLbx.SelectedIndex != -1)
            {
                Purchase(benchLbx, modell.PlayersBench, 0);
            }
        } 

        private void upgrade2Btn_Click(object sender, RoutedEventArgs e)
        {
            if (startingLbx.SelectedIndex != -1)
            {
                Purchase(startingLbx, modell.Players, 1);
            }

            if (benchLbx.SelectedIndex != -1)
            {
                Purchase(benchLbx, modell.PlayersBench, 1);
            }
        }

        private void upgrade3Btn_Click(object sender, RoutedEventArgs e)
        {
            if (startingLbx.SelectedIndex != -1)
            {
                Purchase(startingLbx, modell.Players, 2);
            }

            if (benchLbx.SelectedIndex != -1)
            {
                Purchase(benchLbx, modell.PlayersBench, 2);
            }
        }

        private void upgrade4Btn_Click(object sender, RoutedEventArgs e)
        {
            if (startingLbx.SelectedIndex != -1)
            {
                Purchase(startingLbx, modell.Players, 3);
            }

            if (benchLbx.SelectedIndex != -1)
            {
                Purchase(benchLbx, modell.PlayersBench, 3);
            }
        }

        public void Purchase(ListBox listBox, List<Player> list, int index)
        {
            if(manager.Money >= prices[index])
            {
                if (listBox.SelectedIndex != -1)
                {
                    string lastRating = "" + list[listBox.SelectedIndex].Rating;
                    MessageBoxResult result = MessageBox.Show($"Upgrade {list[listBox.SelectedIndex].Name} for {prices[index]}CQ", "Confirm Upgrade?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes && list[listBox.SelectedIndex].Rating != 99)
                    {
                        if(list[listBox.SelectedIndex].Rating + ratings[index] > 99)
                        {
                            MessageBox.Show("Player cannot exceed 99", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                            list[listBox.SelectedIndex].Rating = 99;
                        }
                        else
                        {
                            list[listBox.SelectedIndex].Rating += ratings[index];
                        }
                        string newRating = "" + list[listBox.SelectedIndex].Rating;
                        string displayString = list[listBox.SelectedIndex].DisplayString;
                        list[listBox.SelectedIndex].DisplayString = displayString.Replace(lastRating, newRating);
                        list[listBox.SelectedIndex].Price = new Calc().getPrice(list[listBox.SelectedIndex].Rating);
                        manager.Money += -prices[index];
                        Serialize(list, manager);
                        listBox.Items.Clear();
                        AddRange(list, listBox);
                        MessageBox.Show("Successfully Upgraded Player", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if(result == MessageBoxResult.No)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Player cannot exceed 99", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    coinAmount.Content = manager.Money + "CQ";
                    
                }
            }
            else
            {
                MessageBox.Show("You don't have enough money!", "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void startingLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(startingLbx.SelectedIndex != -1)
            {
                int index = startingLbx.SelectedIndex;
                Player player = modell.Players[index];
                if (player.Multiupgrade)
                {
                    upgrade4Btn.IsEnabled = true;
                }
                else
                {
                    upgrade4Btn.IsEnabled = false;
                }
                benchLbx.IsEnabled = false;
                benchLbx.SelectedIndex = -1;
            }
        }

        private void benchLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(benchLbx.SelectedIndex != -1)
            {
                int index = benchLbx.SelectedIndex;
                Player player = modell.PlayersBench[index];
                if (player.Multiupgrade)
                {
                    upgrade4Btn.IsEnabled = true;
                }
                else
                {
                    upgrade4Btn.IsEnabled = false;
                }
                startingLbx.IsEnabled = false;
                startingLbx.SelectedIndex = -1;
            }
        }

        public void Serialize(List<Player> list1, Manager manage)
        {
            if(list1 == modell.Players)
            {
                new Persistence().SavePlayersStarting(list1);
            }
            else
            {
                new Persistence().SavePlayersbench(list1);
            }
            new Persistence().SaveManagers(manage);
        }

        private void benchLbx_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            startingLbx.IsEnabled = true;
            benchLbx.IsEnabled = true;
        }

        private void startingLbx_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            startingLbx.IsEnabled = true;
            benchLbx.IsEnabled = true;
        }
    }
}