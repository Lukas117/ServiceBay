using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
            SetAuctionsText();
            SetPersonsText();
        }

        private async void SetAuctionsText()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/");

            HttpResponseMessage result = hc.GetAsync("ApiAuction").Result;

            if (result.IsSuccessStatusCode)
            {
                var displaydata = await result.Content.ReadAsAsync<IEnumerable<Auction>>();
                int auctionsCount = displaydata.ToList().Count;
                auctionsText.Text = auctionsCount.ToString();
            }
        }

        private async void SetPersonsText()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/");

            HttpResponseMessage result = hc.GetAsync("ApiPerson").Result;

            if (result.IsSuccessStatusCode)
            {
                var displaydata = await result.Content.ReadAsAsync<IEnumerable<Person>>();
                int personsCount = displaydata.ToList().Count;
                personsText.Text = personsCount.ToString();
            }
        }
    }
}
