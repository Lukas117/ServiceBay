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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new MainPage();
        }

        private void PersonPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PersonPage();
        }

        private void MainPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new MainPage();
        }

        private void AuctionPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AuctionPage();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Owner = Owner;
            loginWindow.Show();
            Close();
        }
    }
}
