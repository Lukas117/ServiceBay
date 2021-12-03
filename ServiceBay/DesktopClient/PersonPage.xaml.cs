using ServiceBay.Controllers;
using ServiceBay.Dto;
using ServiceBay.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PersonPage.xaml
    /// </summary>
    public partial class PersonPage : Page
    {

        public PersonPage()
        {
            InitializeComponent();
            LoadDataAsync();
        }

        public async void LoadDataAsync()
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/");

            HttpResponseMessage result = hc.GetAsync("ApiPerson").Result;

            if (result.IsSuccessStatusCode)
            {
                var displaydata = await result.Content.ReadAsAsync<IEnumerable<Person>>();
                personTable.ItemsSource = displaydata;
            }
        }

        private void PopUpButton_Click(object sender, RoutedEventArgs e)
        {
            //popup.Closed(name); clear values
            popup.IsOpen = true;
        }

        private void PopUpCloseButton_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = false;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO Dto for API
            PersonForCreationDto person = new PersonForCreationDto { Fname = fname.Text, Lname = lname.Text, Phoneno = phoneno.Text, Email = email.Text, Password = password.Text };
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/");

            HttpResponseMessage result = hc.PostAsJsonAsync<PersonForCreationDto>("ApiPerson", person).Result;

            if (result.IsSuccessStatusCode)
            {
                LoadDataAsync();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Person row = (Person)personTable.SelectedItem;
            int id = row.Id;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44349/api/");

            HttpResponseMessage result = hc.DeleteAsync("ApiPerson/" + id).Result;

            if (result.IsSuccessStatusCode)
            {
                IEnumerable<Person> displaydata = await result.Content.ReadAsAsync<IEnumerable<Person>>();
                personTable.ItemsSource = displaydata;
                personTable.Items.Remove(row);
            }
            LoadDataAsync();
        }

        private void FindText_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBoxName = (TextBox)sender;
            string filterText = textBoxName.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(personTable.ItemsSource);

            if (!string.IsNullOrEmpty(filterText))
            {
                cv.Filter = o =>
                {
                    /* change to get data row value */
                    Person a = o as Person;
                    return (a.Email.ToUpper().StartsWith(filterText.ToUpper()));
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