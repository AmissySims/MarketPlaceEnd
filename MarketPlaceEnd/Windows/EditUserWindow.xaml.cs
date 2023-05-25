using MarketPlaceEnd.Models;
using System;
using System.Collections.Generic;
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
            contextUser = user;
            DataContext = contextUser;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
       DialogResult = true;

        private void AddImageBt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contextUser.Name))
            {
                MessageBox.Show("Заполните поле названия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(contextUser.FName))
            {
                MessageBox.Show("Заполните поле описания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(contextUser.Login))
            {
                MessageBox.Show("Заполните поле описания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
               
                App.db.SaveChanges();
                MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
        }
    }
}
