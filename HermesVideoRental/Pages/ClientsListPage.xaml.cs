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
    /// Interaction logic for ClientsListPage.xaml
    /// </summary>
    public partial class ClientsListPage : Page
    {
        int _pageNumber = 0;
        int _recordsCountShown = 0;

        public List<Client> ClientsList { get; set; }

        private Predicate<Client> _genderFilter = x => true;
        private Predicate<Client> _birthDateFilter = x => true;

        private Func<Client, object> _sortingQuery = x => x.Id;

        public ClientsListPage()
        {
            InitializeComponent();
        }

        private void UpdateClients()
        {
            ClientsList = App.Connection.Client.ToList()
                .Where(x => _genderFilter(x) && _birthDateFilter(x))
                .OrderBy(x => _sortingQuery(x))
                .ToList();

            SearchClients();
        }

        private void SearchClients()
        {
            lvClients.ItemsSource = ClientsList
                .Where(x => string.Join(" ", x.Firstname, x.Lastname,
                x.Patronymic, x.Email, x.PhoneNumber)
                .ToLower()
                .Contains(tbSearch.Text));
            GetClientsByPages();
        }

        private void GetClientsByPages()
        {
            ClientsList = ClientsList.Skip(_pageNumber * _recordsCountShown).Take(_pageNumber* _recordsCountShown).ToList();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            CbGender.SelectedIndex = 2;
            CbSort.SelectedIndex = 3;
            CbGender.SelectedIndex = 3;

            UpdateClients();
        }

        private void CbNumberOfRecordsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CbNumberOfRecords.SelectedIndex)
            {
                case 0:
                    _recordsCountShown = 10;
                    break;
                case 1:
                    _recordsCountShown = 50;
                    break;
                case 2:
                    _recordsCountShown = 200;
                    break;
                default:
                    _recordsCountShown = App.Connection.Client.Count();
                    break;
            }
            UpdateClients();
        }

        private void CbGenderSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CbGender.SelectedIndex)
            {
                case 0:
                    _genderFilter = x => x.Gender.ID == "М";
                    break;

                case 1:
                    _genderFilter = x => x.Gender.ID == "Ж";
                    break;

                default:
                    _genderFilter = x => true;
                    break;
            }

            UpdateClients();
        }

        private void CbSortSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CbSort.SelectedIndex)
            {
                case 0:
                    _sortingQuery = x => x.Firstname;
                    break;

                case 1:
                    _sortingQuery = x => DateTime.MaxValue - x.LastVisitDate;
                    break;

                case 2:
                    _sortingQuery = x => -x.CountOfVisits;
                    break;

                default:
                    _sortingQuery = x => x.Id;
                    break;
            }

            UpdateClients();
        }

        private void TbSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchClients();
        }

        private void ChbBirthDateClick(object sender, RoutedEventArgs e)
        {
            if (ChbBirthDate.IsChecked == true)
            {
                _birthDateFilter = x => x.BirthDate.Month == DateTime.Now.Month;
            }
            else
            {
                _birthDateFilter = x => true;
            }
            UpdateClients();
        }

        private void BtnPreviousPageClick(object sender, RoutedEventArgs e)
        {
            ChangePage(-1);
        }

        private void BtnNextPageClick(object sender, RoutedEventArgs e)
        {
            ChangePage(1);
        }

        private void ChangePage(int pageNumber)
        {
            var numberOfClients = App.Connection.Client.Count();
            var numberOfPages = numberOfClients % _recordsCountShown == 0 
                ? numberOfClients / _recordsCountShown : numberOfClients / _recordsCountShown + 1;

            _pageNumber += pageNumber;

            if(_pageNumber <= 0)
            {
                _pageNumber = 0;
                BtnPreviousPage.IsEnabled = false;
            }
            else
            {
                BtnPreviousPage.IsEnabled = true;
            }

            if(_pageNumber >= numberOfPages - 1)
            {
                BtnNextPage.IsEnabled = false;
            }
            else
            {
                BtnNextPage.IsEnabled = true;
            }
        }
    }
}
