using HermesVideoRental.Components;
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

namespace HermesVideoRental.Pages
{
    /// <summary>
    /// Interaction logic for ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        Client _client;
        bool isEdit = false;
        public ClientPage(Client client)
        {
            InitializeComponent();

            if (client != null)
            {
                _client = client;
                isEdit= true;
            }
            else
            {
                _client = new Client();
            }

            DataContext= _client;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            tbId.Visibility = isEdit? Visibility.Visible : Visibility.Collapsed;
            tblId.Visibility = isEdit? Visibility.Visible : Visibility.Collapsed;
            CbGender.ItemsSource = App.Connection.Gender.ToList();
        }
    }
}
