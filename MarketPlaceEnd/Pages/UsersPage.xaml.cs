using MarketPlaceEnd.Models;
using MarketPlaceEnd.Windows;
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

namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            var use = App.db.User.ToList();
            UsersList.ItemsSource = use;
        }

        private void EditBt_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow selUser = new EditUserWindow((sender as Button).DataContext as User);
            selUser.ShowDialog();
            UsersList.ItemsSource = App.db.User.ToList();
        }

        private void DeleteBt_Click(object sender, RoutedEventArgs e)
        {
            var selUsers = (sender as Button).DataContext as User;
            var selOrder = selUsers.Order;
            var selCard = selUsers.Cards;
            App.db.Order.RemoveRange(selOrder);
            App.db.Cards.RemoveRange(selCard);
            App.db.User.Remove(selUsers);
            App.db.SaveChanges();
            UsersList.ItemsSource = App.db.User.ToList();
        }
        private void AddUserBt_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow selUser = new EditUserWindow(new User());
            selUser.ShowDialog();
            UsersList.ItemsSource = App.db.User.ToList();
        }
    }

}
