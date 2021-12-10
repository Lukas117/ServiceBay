using ServiceBay.Dto;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for AuctionPage.xaml
    /// </summary>
    public partial class AuctionPage : Page
    {
        public AuctionPage()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        public async void LoadDataAsync()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/");

            HttpResponseMessage result = hc.GetAsync("ApiAuction").Result;

            if (result.IsSuccessStatusCode)
            {
                var displaydata = await result.Content.ReadAsAsync<IEnumerable<Auction>>();
                auctionTable.ItemsSource = displaydata;
            }
        }

        private void PopUpOpenButton_Click(object sender, RoutedEventArgs e)
        {
            popupCreate.IsOpen = true;
        }

        private void PopUpCloseButton_Click(object sender, RoutedEventArgs e)
        {
            popupCreate.IsOpen = false;
        }

        private void PopUpUpdateOpenButton_Click(object sender, RoutedEventArgs e)
        {
            //popup.Closed(name); clear values
            Auction row = (Auction)auctionTable.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Select auction!", "Select", MessageBoxButton.OK, MessageBoxImage.Information);
                popupUpdate.IsOpen = false;
            }
            else
            {
                popupUpdate.IsOpen = true;
                aname.Text = row.AuctionName;
                adescription.Text = row.AuctionDescription;
                aendDate.Text = row.EndDate.ToString();
            }
        }

        private void PopUpUpdateCloseButton_Click(object sender, RoutedEventArgs e)
        {
            popupUpdate.IsOpen = false;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            AuctionForCreationDto auction = new AuctionForCreationDto { AuctionName = name.Text, AuctionDescription = description.Text, StartingDate = startingDate.Value.Value, EndDate = endDate.Value.Value, StartingPrice = double.Parse(startingPrice.Text), SellerId = int.Parse(sellerId.Text) };
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/");

            HttpResponseMessage result = hc.PostAsJsonAsync<AuctionForCreationDto>("ApiAuction", auction).Result;

            if (result.IsSuccessStatusCode)
            {
                LoadDataAsync();
                popupCreate.IsOpen = false;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Auction row = (Auction)auctionTable.SelectedItem;
            int id = row.Id;
            AuctionForUpdateDto auction = new AuctionForUpdateDto { Id = id, AuctionName = aname.Text, AuctionDescription = adescription.Text, EndDate = aendDate.Value.Value };
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/");

            HttpResponseMessage result = hc.PutAsJsonAsync<AuctionForUpdateDto>("ApiAuction/" + id, auction).Result;

            if (result.IsSuccessStatusCode)
            {
                LoadDataAsync();
                popupUpdate.IsOpen = false;
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Auction row = (Auction)auctionTable.SelectedItem;
            if (row == null)
            {
                MessageBox.Show("Select auction!", "Select", MessageBoxButton.OK, MessageBoxImage.Information);
                popupUpdate.IsOpen = false;
            }
            else
            {
                int id = row.Id;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44349/api/");

                HttpResponseMessage result = hc.DeleteAsync("ApiAuction/" + id).Result;

                if (result.IsSuccessStatusCode)
                {
                    IEnumerable<Auction> displaydata = await result.Content.ReadAsAsync<IEnumerable<Auction>>();
                    auctionTable.ItemsSource = displaydata;
                    auctionTable.Items.Remove(row);
                }
                LoadDataAsync();
            }
        }

        private void FindText_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBoxName = (TextBox)sender;
            string filterText = textBoxName.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(auctionTable.ItemsSource);

            if (!string.IsNullOrEmpty(filterText))
            {
                cv.Filter = o => {
                    /* change to get data row value */
                    Auction a = o as Auction;
                    return a.AuctionName.ToUpper().StartsWith(filterText.ToUpper());
                    /* end change to get data row value */
                };
            }
            else
            {
                LoadDataAsync();
            }
        }
    }
}
