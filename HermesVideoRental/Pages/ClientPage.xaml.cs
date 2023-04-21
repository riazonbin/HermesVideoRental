using HermesVideoRental.Components;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                isEdit = true;
            }
            else
            {
                _client = new Client();
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = _client;

            tbId.Visibility = isEdit ? Visibility.Visible : Visibility.Collapsed;
            tblId.Visibility = isEdit ? Visibility.Visible : Visibility.Collapsed;
            CbGender.ItemsSource = App.Connection.Gender.ToList();
            ShowFreeTags();
        }

        private void ShowFreeTags()
        {
            var freeTags = new List<Tag>();
            var allTags = App.Connection.Tag.ToList();

            foreach (var clientTag in _client.ClientTag)
            {
                allTags.Remove(clientTag.Tag);
            }

            freeTags = allTags;
            lvFreeTags.ItemsSource = freeTags;
        }

        private void UpdateUserTags()
        {
            lvClientTags.Items.Refresh();
            ShowFreeTags();
        }

        private void LvFreeTagsMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var tag = ((ListView)sender).SelectedItem as Tag;

                if (tag == null)
                {
                    return;
                }

                var clientTag = new ClientTag()
                {
                    Tag = tag,
                    Client = _client
                };

                _client.ClientTag.Add(clientTag);

                UpdateUserTags();
            }
            catch
            {
                ShowErrorMessage();
            }
        }

        private static void ShowErrorMessage()
        {
            MessageBox.Show($"Произошла ошибка, попробуйте еще раз!",
        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void lvClientTagsMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var clientTag = ((ListView)sender).SelectedItem as ClientTag;

                if(clientTag == null)
                {
                    return;
                }

                _client.ClientTag.Remove(clientTag);

                if (clientTag.Id != 0)
                {
                    App.Connection.ClientTag.Remove(clientTag);
                    App.Connection.SaveChanges();
                }

                UpdateUserTags();
            }
            catch 
            {
                ShowErrorMessage();
            }
        }

        private void BtnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!IsDataCorrect())
                {
                    MessageBox.Show($"Данные введены некорекктно!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                App.Connection.Client.AddOrUpdate(_client);
                App.Connection.SaveChanges();

                if (!isEdit)
                {
                    MessageBox.Show($"Клиент {_client.Firstname} {_client.Lastname} {_client.Patronymic} успешно добавлен в систему!",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Клиент {_client.Firstname} {_client.Lastname} {_client.Patronymic} успешно изменен в системе!",
                        "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                NavigationService.GoBack();
            }
            catch 
            {
                ShowErrorMessage();
            }
        }

        private void PageUnloaded(object sender, RoutedEventArgs e)
        {
            if(isEdit)
            {
                App.Connection.Entry(_client).Reload();
            }
        }

        public bool IsDataCorrect()
        {
            if(_client.BirthDate == null || _client.BirthDate == DateTime.MinValue)
            {
                return false;
            }

            var emailCheck = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            var phoneCheck = new Regex(@"^[+]?[7-8][0-9]{10}$");
            var namesCheck = new Regex(@"^[A-Za-zА-Яа-я -]+$");

            if (emailCheck.IsMatch(_client.Email)
                && phoneCheck.IsMatch(_client.PhoneNumber)
                && namesCheck.IsMatch(_client.Firstname)
                && namesCheck.IsMatch(_client.Lastname)
                && namesCheck.IsMatch(_client.Patronymic))
            {
                return true;
            }

            return false;
        }
    }
}
