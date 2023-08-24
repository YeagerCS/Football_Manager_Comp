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
using Microsoft.WindowsAPICodePack.Dialogs;
using WindowsAPICodePack.Dialogs;

namespace Football_Manager
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        Reference reference = new Reference();
        Team team = new Team();
        public WelcomeWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            pathTbx.Text = "C:/Users/" + Environment.UserName + "/";
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(firstnameTbx.Text) || string.IsNullOrEmpty(lastnameTbx.Text)) return;

            if (!string.IsNullOrEmpty(pathTbx.Text))
            {
                new Persistence().SavePath(pathTbx.Text);
            }
            reference.manager = new Manager(firstnameTbx.Text, lastnameTbx.Text);
            reference.manager.Money = 1000;
            reference.manager.Rating = 5;
            team = new Team(teamnameTbx.Text, 0, 1);

            reference.manager.TeamAssigned = team;
            new Persistence().SaveManagers(reference.manager);
            new Persistence().SaveTeam(team);
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private string ShowDirectoryDialog()
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                CommonFileDialogResult result = dialog.ShowDialog();

                if (result == CommonFileDialogResult.Ok)
                {
                    return dialog.FileName;
                }
            }

            return null;
        }

        private void choosefileBtn_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = ShowDirectoryDialog();
            if (!string.IsNullOrEmpty(folderPath))
            {
                pathTbx.Text = $"{folderPath}\\";
            }
            else
            {
                pathTbx.Text = "C:/Users/" + Environment.UserName + "\\";
            }
        }
    }
}
