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
using MarketPlaceEnd.Windows;
using MarketPlaceEnd.Models;
using MarketPlaceEnd.Pages;



namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для PersAccountPage.xaml
    /// </summary>
    public partial class PersAccountPage : Page
    {
        public static List<User> UserList { get; set; }
        public PersAccountPage()
        {
           
            InitializeComponent();
            UserList = App.db.User.Where(z => z.Id == Account.AuthUser.Id).ToList();
            ListUse.ItemsSource = UserList;


        }

        private void EditBt_Click(object sender, RoutedEventArgs e)
        {
            
            EditUserWindow selUser = new EditUserWindow((sender as Button).DataContext as User);
            selUser.ShowDialog();
            UserList = App.db.User.Where(z => z.Id == Account.AuthUser.Id).ToList();
            ListUse.ItemsSource = UserList;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ListUse.ItemsSource = UserList;

        }

        private void OrdersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }
        private void StatBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GrafficsPage());
        }

        private void AddUserBt_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow selUser = new EditUserWindow(new User());
            selUser.ShowDialog();
            UserList = App.db.User.Where(z => z.Id == Account.AuthUser.Id).ToList();
            ListUse.ItemsSource = UserList;
        }
    }
}
