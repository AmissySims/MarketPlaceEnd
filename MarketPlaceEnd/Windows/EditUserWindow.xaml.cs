using MarketPlaceEnd.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarketPlaceEnd.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        User contextUser;
        public EditUserWindow(User user)
        {
            InitializeComponent();
            var roles = App.db.Role.ToList();
            RoleCb.ItemsSource = roles;
            contextUser = user;
            DataContext = contextUser;
            
            if(contextUser.Id == 0 ) 
            {
                RoleStack.Visibility = Visibility.Visible;
            }
            else
            {
                RoleStack.Visibility = Visibility.Collapsed;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
       DialogResult = true;

        private void AddImageBt_Click(object sender, RoutedEventArgs e)
        {   
            try
            {
                var dialog = new OpenFileDialog();
                if (dialog.ShowDialog().GetValueOrDefault())
                {
                    contextUser.Photo = File.ReadAllBytes(dialog.FileName);
                    DataContext = null;
                    DataContext = contextUser;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Ошибка при добавлении картинки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

           
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(contextUser.Name))
                {
                    MessageBox.Show("Заполните поле имени", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextUser.FName))
                {
                    MessageBox.Show("Заполните поле фамилии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextUser.Login))
                {
                    MessageBox.Show("Заполните поле логина", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextUser.Password))
                {
                    MessageBox.Show("Заполните поле пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (contextUser.Id == 0)
                    {
                        App.db.User.Add(contextUser);
                    }
                    App.db.SaveChanges();
                    MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }
    }
}
