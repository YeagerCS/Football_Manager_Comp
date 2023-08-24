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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Football_Manager  
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Manager manager = new Manager();
        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            if (!File.Exists(new Persistence().GetPath() + "serialization\\manager.json"))
            {
                WelcomeWindow welcomeWindow = new WelcomeWindow();
                welcomeWindow.Show();
                this.Close();
            }
            else
            {
                manager = new Persistence().LoadManagers();
                coinLbl.Content = manager.Money + "CQ";
            }
        }

        private void managementBtn_Click(object sender, RoutedEventArgs e)
        {
            TeamManagement teamManager = new TeamManagement();
            teamManager.Show();
            this.Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void upgradeBtn_Click(object sender, RoutedEventArgs e)
        {
            UpgradePlayers window = new UpgradePlayers();
            window.Show();
            this.Close();
        }

        private void shopBtn_Click(object sender, RoutedEventArgs e)
        {
            Shop window = new Shop();
            window.Show();
            this.Close();
        }

        private void statsBtn_Click(object sender, RoutedEventArgs e)
        {
            Stats window = new Stats();
            window.Show();
            this.Close();
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            GameLoop gameLoop = new GameLoop();
            gameLoop.Show();
            this.Close();
        }

        private void matchesBtn_Click(object sender, RoutedEventArgs e)
        {
            Matches matches = new Matches();
            matches.Show();
            this.Close();
        }

        private void debug_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DebuggingWindow teamManagement = new DebuggingWindow();
            teamManagement.Show();
            Close();
        }

    }
}
