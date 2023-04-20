using HermesVideoRental.Components;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод для регистрации пользователя по нажатию кнопки "Зарегистрироваться"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegistrateClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLogin.Text))
            {
                MessageBox.Show("Введите логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(pbPassword.Password))
            {
                MessageBox.Show("Введите пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            var user = App.Connection.User.FirstOrDefault(x => x.Login == tbLogin.Text);

            if (user != null)
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsPasswordCorrect(pbPassword.Password))
            {
                MessageBox.Show("Пароль не подходит под критерии! \nОн должен быть как минимум 6 символов в длину, содержать 1 цифру, 1 латинскую прописную букву," +
                    " и содержать один из следующих символов: ! @ # $ % ^ ",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new User()
            {
                Login = tbLogin.Text,
                Password = pbPassword.Password,
            };

            App.Connection.User.Add(newUser);
            App.Connection.SaveChanges();

            MessageBox.Show("Вы успешно зарегистрированы!", "Успех", MessageBoxButton.OK, MessageBoxImage.Error);
            NavigationService.Navigate(new AuthorizationPage());
        }


        public bool IsPasswordCorrect(string password)
        {
            var minPasswordLength = 6;
            var digitCheck = new Regex(@"\d+");
            var capitalLetterCheck = new Regex(@"[A-Z]+");
            var specialSymbolCheck = new Regex(@"[!@#$%^]+");

            if (password.Length >= minPasswordLength
                && digitCheck.IsMatch(password)
                && capitalLetterCheck.IsMatch(password)
                && specialSymbolCheck.IsMatch(password))
            {
                return true;
            }

            return false;
        }
    }
}
