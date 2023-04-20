﻿using System;
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
        public ClientsListPage()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            lvClients.ItemsSource = App.Connection.Client.ToList();
        }
    }
}
