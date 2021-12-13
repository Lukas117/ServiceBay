using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace DesktopClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static string tokenbased;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login() { Email = emailText.Text, Password = passwordText.Text };
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/");
            hc.DefaultRequestHeaders.Clear();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = hc.PostAsJsonAsync<Login>("ApiAuthentication/UserLogin", login);
            response.Wait();

            var saved = response.Result;
            if (saved.IsSuccessStatusCode)
            {
                var responseMessage = response.Result.Content.ReadAsStringAsync().Result;
                tokenbased = JsonConvert.DeserializeObject<string>(responseMessage);
                hc.DefaultRequestHeaders.TryAddWithoutValidation("UserToken", tokenbased);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Owner = Owner;
                mainWindow.Show();
                Close();
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}