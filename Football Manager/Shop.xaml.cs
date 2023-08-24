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
    /// Interaction logic for Shop.xaml
    /// </summary>
    public partial class Shop : Window
    {
        Manager manager = new Manager();
        Modell modell = new Modell();
        List<Player> shop = new List<Player>();
        public Shop()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            manager = new Persistence().LoadManagers();
            modell.Players = new Persistence().LoadPlayersStarting();
            modell.PlayersBench = new Persistence().LoadPlayersBench();
            coinLbl.Content = manager.Money + "CQ";

            if (!File.Exists(new Persistence().GetPath() + "serialization\\shop.json"))
            {
                shop = GenerateShop();
                new Persistence().SavePlayerShop(shop);
            }
            else
            {
                shop = new Persistence().LoadPlayerShop();
            }

            if(shop.Count == 0)
            {
                shop = GenerateShop();
                new Persistence().SavePlayerShop(shop);
            }
            AddRange(shopLbx, shop);
        }

        public void AddRange(ListBox listBox, List<Player> list)
        {
            foreach(Player player in list)
            {
                listBox.Items.Add(player);
            }
        }
        public List<Player> GenerateShop()
        {
            string[] firstNames = { "Firefox", "Chrome", "Esse", "Eater", "Osborn", "Citro", "Neymar", "Snickers", "Mario", "Aeberhard", "Zivkovic", "Mike", "Benjamin", "Sneaker", "Choc", "Baller", "Opera", "Lessio", "Damjan", "Marin", "Schnitzel", "Enjoyer", "Slayer", "Kayali", "Kaan", "Milk", "Shaker", "Slapper", "Monitor", "GTX", "Fortnite", "Stutz", "Icetea", "Sabbi", "Wallflower", "DiCaprio", "Stuff", "Smelter", "White", "Pinkman", "Rolando", "Silvan", "Velo", "Katz", "Bonuty", "Anes", "Driver", "Controller", "Serkan", "Jurassic", "Schoki", "Obren", "Andrew", "Garfield", "Dielochis" };
            string[] lastNames = firstNames;

            string[] positions = { "GK", "LB", "CB", "CB", "RB", "CDM", "CM", "CAM", "LM", "RM", "LW", "RW", "CF", "ST" };

            Random rand = new Random();

            List<Player> players = new List<Player>();

            for(int i = 0; i <= 4; i++)
            {
                string name = firstNames[rand.Next(firstNames.Length)] + " " + lastNames[rand.Next(lastNames.Length)];
                int rating = rand.Next(75, 90);
                int shirtNumber = 0;
                string position = positions[rand.Next(positions.Length)];

                int price = new Calc().getPrice(rating);

                players.Add(new Player(name, rating, shirtNumber, position, price, rand.Next(8) == 0));
            }

            return players;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void purchaseBtn_Click(object sender, RoutedEventArgs e)
        {
            if(shopLbx.SelectedIndex != -1)
            {
                if(manager.Money >= shop[shopLbx.SelectedIndex].Price)
                {
                    MessageBoxResult result = MessageBox.Show("Confirm the Purchase of " + shop[shopLbx.SelectedIndex].Name + " for " + shop[shopLbx.SelectedIndex].Price + "CQ?", "Confirm?", MessageBoxButton.YesNo, MessageBoxImage.Question); 
                    if(result == MessageBoxResult.Yes)
                    {
                        Player player = shop[shopLbx.SelectedIndex];
                        //Give Player a shirtNumber
                        player.ShirtNumber = new Random().Next(1, 41);
                        while(modell.Players.Exists(p => p.ShirtNumber == player.ShirtNumber))
                        {
                            shop[shopLbx.SelectedIndex].ShirtNumber = new Random().Next(1, 41);
                        }
                        player.DisplayString = string.Concat(player.ShirtNumber, player.DisplayString.Substring(1));

                        //Execute Purchase
                        manager.Money -= shop[shopLbx.SelectedIndex].Price;
                        modell.PlayersBench.Add(player);
                        shop.RemoveAt(shopLbx.SelectedIndex);
                        shopLbx.Items.Clear();
                        AddRange(shopLbx, shop);
                        new Persistence().SavePlayerShop(shop);
                        new Persistence().SavePlayersbench(modell.PlayersBench);
                    }
                    coinLbl.Content = manager.Money + "CQ";
                    priceLbl.Content = "";
                    MessageBox.Show("Successfully purchased Player", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    new Persistence().SaveManagers(manager);
                }
                else
                {
                    MessageBox.Show("You don't have enough money!", "Insufficient funds", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void shopLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(shopLbx.SelectedIndex != -1)
            {
                priceLbl.Content = shop[shopLbx.SelectedIndex].Price + "CQ";
            }
        }

        private void packsBtn_Click(object sender, RoutedEventArgs e)
        {
            Packs packsWindow = new Packs();
            packsWindow.Show();
            this.Close();
        }
    }
}
