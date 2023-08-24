using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Packs.xaml
    /// </summary>
    public partial class Packs : Window
    {
        Persistence persServ = new Persistence();
        Manager manager = new Persistence().LoadManagers();
        int sellingPrice;
        List<Player> currentPacked;
        Pack[] packs;
        Button[] purchaseButtons;
        public Packs()
        {
            InitializeComponent();
            coinLbl.Content = manager.Money + "CQ";
            Init();
        }

        private void Init()
        {
            Pack[] packst =
            {
                new Pack(25000, 5, Rarity.Common),
                new Pack(32000, 5, Rarity.Average),
                new Pack(48000, 6, Rarity.Aight),
                new Pack(70000, 5, Rarity.Rare),
                new Pack(100000, 5, Rarity.VeryRare),
                new Pack(160000, 5, Rarity.Legendary),
            };

            packs = packst;

            Label[] labels =
            {
                priceLbl, priceLbl1, priceLbl2, priceLbl3, priceLbl4, priceLbl5
            };

            int i = 0;
            foreach (Label label in labels)
            {
                label.Content = packst[i].Price + "CQ";
                i++;
            }

            Button[] prcs =
            {
                purchaseBtn0,
                purchaseBtn1,
                purchaseBtn2,
                purchaseBtn3,
                purchaseBtn4,
                purchaseBtn5,
            };

            purchaseButtons = prcs;
        }


        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void SwitchPurchaseEnabled(bool enabled)
        {
            foreach(Button button in purchaseButtons)
            {
                button.IsEnabled = enabled;
            }
        }

        private void shopLbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool ApprovePurchase(int price)
        {
            if (manager.Money < price)
            {
                MessageBox.Show("You don't have enough CQ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            MessageBoxResult boxResult = MessageBox.Show("Buy for " + price + "CQ?","Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return boxResult == MessageBoxResult.Yes;
        }

        private void CompletePurchase(int price)
        {
            manager.Money -= price;
            persServ.SaveManagers(manager);
            coinLbl.Content = manager.Money + "CQ";
        }
        private void AppendPlayersToLbx(ListBox listBox, List<Player> playerList)
        {
            foreach(Player player in playerList)
            {
                listBox.Items.Add(player);
            }
        }

        private void CalculateSellingPrice(List<Player> players)
        {
            int addedPrice = 0;
            foreach(Player player in players)
            {
                addedPrice += (player.Price - new Calc().GeneratePriceRemoval(player.Rating));
            }

            sellingPrice = addedPrice;
            sellLbl.Content = addedPrice + "CQ";
        }

        private void purchaseBtn0_Click(object sender, RoutedEventArgs e)
        {
            if (ApprovePurchase(packs[0].Price))
            {
                CompletePurchase(packs[0].Price);
                packsLbx.Items.Clear();
                List<Player> packedPlayers = packs[0].OpenPack();
                AppendPlayersToLbx(packsLbx, packedPlayers);
                SwitchPurchaseEnabled(false);
                currentPacked = packedPlayers;
                CalculateSellingPrice(packedPlayers);
            }
            
        }


        private void purchaseBtn1_Click(object sender, RoutedEventArgs e)
        {
            if (ApprovePurchase(packs[1].Price))
            {
                CompletePurchase(packs[1].Price);
                packsLbx.Items.Clear();
                List<Player> packedPlayers = packs[1].OpenPack();
                AppendPlayersToLbx(packsLbx, packedPlayers);
                SwitchPurchaseEnabled(false);
                currentPacked = packedPlayers;
                CalculateSellingPrice(packedPlayers);
            }
        }

        private void purchaseBtn2_Click(object sender, RoutedEventArgs e)
        {
            if (ApprovePurchase(packs[2].Price))
            {
                CompletePurchase(packs[2].Price);
                packsLbx.Items.Clear();
                List<Player> packedPlayers = packs[2].OpenPack();
                AppendPlayersToLbx(packsLbx, packedPlayers);
                SwitchPurchaseEnabled(false);
                currentPacked = packedPlayers;
                CalculateSellingPrice(packedPlayers);
            }
        }

        private void purchaseBtn3_Click(object sender, RoutedEventArgs e)
        {
            if (ApprovePurchase(packs[3].Price))
            {
                CompletePurchase(packs[3].Price);
                packsLbx.Items.Clear();
                List<Player> packedPlayers = packs[3].OpenPack();
                AppendPlayersToLbx(packsLbx, packedPlayers);
                SwitchPurchaseEnabled(false);
                currentPacked = packedPlayers;
                CalculateSellingPrice(packedPlayers);
            }
        }

        private void purchaseBtn4_Click(object sender, RoutedEventArgs e)
        {
            if (ApprovePurchase(packs[4].Price))
            {
                CompletePurchase(packs[4].Price);
                packsLbx.Items.Clear();
                List<Player> packedPlayers = packs[4].OpenPack();
                AppendPlayersToLbx(packsLbx, packedPlayers);
                SwitchPurchaseEnabled(false);
                currentPacked = packedPlayers;
                CalculateSellingPrice(packedPlayers);
            }
        }

        private void purchaseBtn5_Click(object sender, RoutedEventArgs e)
        {
            if (ApprovePurchase(packs[5].Price))
            {
                CompletePurchase(packs[5].Price);
                packsLbx.Items.Clear();
                List<Player> packedPlayers = packs[5].OpenPack();
                AppendPlayersToLbx(packsLbx, packedPlayers);
                SwitchPurchaseEnabled(false);
                currentPacked = packedPlayers;
                CalculateSellingPrice(packedPlayers);
            }
        }

        private void pushBtn_Click(object sender, RoutedEventArgs e)
        {
            if(currentPacked.Count < 1)
            {
                return;
            }
            Random rand = new Random();
            //Save Pulled Players to the Bench

            List<Player> startPlayers = persServ.LoadPlayersStarting();
            List<Player> prevPlayers = persServ.LoadPlayersBench();
            List<Player> totalPlayers = startPlayers;


            foreach(Player player in prevPlayers)
            {
                totalPlayers.Add(player);
            }


            //Handle maximum team size
            if(totalPlayers.Count + currentPacked.Count >= 41)
            {
                MessageBox.Show("Team Size can't exceed 41 players.");
                return;
            }

            foreach (Player player in currentPacked)
            {
                //Configure Shirt Number
                int shirtNumber = rand.Next(1, 41);
                while (totalPlayers.Exists(p => p.ShirtNumber == shirtNumber))
                {
                    shirtNumber = rand.Next(1, 41);
                }

                player.ShirtNumber = shirtNumber;
                player.ReloadDisplay();

                prevPlayers.Add(player);
            }
            persServ.SavePlayersbench(prevPlayers);
            packsLbx.Items.Clear();
            SwitchPurchaseEnabled(true);
            MessageBox.Show("Players added to Team");
            sellLbl.Content = "##";
        }

        private void ApplyQuicksell()
        {
            manager.Money += sellingPrice;
            coinLbl.Content = manager.Money + "CQ";
            packsLbx.Items.Clear();
            sellLbl.Content = "##";
        }

        private void quickSellBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult approved = MessageBox.Show("Confirm Quicksell for " + sellingPrice + "CQ", "Confirm Quicksell", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(approved == MessageBoxResult.Yes)
            {
                if (currentPacked.Count < 1) return;
                ApplyQuicksell();
                SwitchPurchaseEnabled(true);
            }
            
        }
    }
}