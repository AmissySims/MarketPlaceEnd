using MarketPlaceEnd.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void RegBt_Click(object sender, RoutedEventArgs e)
        {
            var check = App.db.User.FirstOrDefault(x => x.Login == LoginTb.Text);
            if (string.IsNullOrWhiteSpace(NameTb.Text.Trim()))
            {
                MessageBox.Show("Заполните поле имени", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(LoginTb.Text.Trim()))
            {
                MessageBox.Show("Заполните поле логина", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(PasswordTb.Text.Trim()))
            {
                MessageBox.Show("Заполните поле пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(FNameTb.Text.Trim()))
            {
                MessageBox.Show("Заполните поле фамилии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
          
            if (check == null)
            {

                User user = new User
                {
                    Name = NameTb.Text,
                    Password = PasswordTb.Text,
                    FName = FNameTb.Text,
                    RoleId = 3,
                    Login = LoginTb.Text
                };
                App.db.User.Add(user);
                App.db.SaveChanges();
                MessageBox.Show("Регистрация выполнена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NameTb.Text = "";
                PasswordTb.Text = "";
                FNameTb.Text = "";
                LoginTb.Text = "";
            }
            else
            {
                MessageBox.Show("Пользователь уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           
        }

        private void EnterBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void NameTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void FNameTb_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
