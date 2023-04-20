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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public int authorizationAttemptsCount = 0;
        public const int maxAuthorizationAttempts = 3;

        public AuthorizationPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Метод для авторизации пользователя по нажатию кнопки "Авторизоваться"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAuthorizeClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.DateOfUnblock > DateTime.Now)
            {
                MessageBox.Show("В связи с многократным вводом некорректных данных Вы были заблокированы на 1 минуту!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                MessageBox.Show("Введите логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(string.IsNullOrWhiteSpace(pbPassword.Password)) 
            {
                MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = App.Connection.User.FirstOrDefault(x => x.Login == tbLogin.Text && x.Password == pbPassword.Password);

            if(user == null) 
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                authorizationAttemptsCount++;

                if(authorizationAttemptsCount > maxAuthorizationAttempts)
                {
                    Properties.Settings.Default.DateOfUnblock = DateTime.Now.AddMinutes(1);
                    Properties.Settings.Default.Save();
                    authorizationAttemptsCount = 0;
                }

                return;
            }
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
