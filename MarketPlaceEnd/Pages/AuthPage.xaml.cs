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

namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void RegBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());

        }

        private void EnterBt_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginTb.Text;
            var password = PasswordTb.Text;
            var user = App.db.User.FirstOrDefault(us => us.Login == login && us.Password == password);
            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Заполните поле логина", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните поле пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (user == null)
            {
                MessageBox.Show("Такого пользователя не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }
            
           
            else
            {
                MessageBox.Show("Вход выполнен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new MainPage());
            }
           
        }
    }
}
