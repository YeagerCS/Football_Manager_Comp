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
    /// Interaction logic for Matches.xaml
    /// </summary>
    public partial class Matches : Window
    {
        Modell modell = new Modell();
        public Matches()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            AddRange(modell.matches);
        }

        public void AddRange(List<string> items)
        {
            foreach(string str in items)
            {
                lbx.Items.Add(str);
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
