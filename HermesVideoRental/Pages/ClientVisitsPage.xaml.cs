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
    /// Interaction logic for ClientVisitsPage.xaml
    /// </summary>
    public partial class ClientVisitsPage : Page
    {
        Client _client;

        Predicate<Visit> visitDayOfWeekPredicate = x => true;
        public ClientVisitsPage(Client client)
        {
            InitializeComponent();

            _client = client;

            CbDayOfWeeks.SelectedIndex = 7;

            UpdateData();
        }

        private void CbDayOfWeeksSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CbDayOfWeeks.SelectedIndex)
            {
                case 0: visitDayOfWeekPredicate = x => x.Date.DayOfWeek == DayOfWeek.Monday;
                    break;

                case 1:
                    visitDayOfWeekPredicate = x => x.Date.DayOfWeek == DayOfWeek.Tuesday;
                    break;

                case 2:
                    visitDayOfWeekPredicate = x => x.Date.DayOfWeek == DayOfWeek.Wednesday;
                    break;

                case 3:
                    visitDayOfWeekPredicate = x => x.Date.DayOfWeek == DayOfWeek.Thursday;
                    break;

                case 4:
                    visitDayOfWeekPredicate = x => x.Date.DayOfWeek == DayOfWeek.Friday;
                    break;

                case 5:
                    visitDayOfWeekPredicate = x => x.Date.DayOfWeek == DayOfWeek.Saturday;
                    break;

                case 6:
                    visitDayOfWeekPredicate = x => x.Date.DayOfWeek == DayOfWeek.Sunday;
                    break;

                default:
                    visitDayOfWeekPredicate = x => true;
                    break;
            }

            UpdateData();
        }

        private void UpdateData()
        {
           lvVisits.ItemsSource = _client.Visit.Where(x => visitDayOfWeekPredicate(x));
        }
    }
}
