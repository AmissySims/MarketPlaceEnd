using MarketPlaceEnd.Models;
using MarketPlaceEnd.Windows;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        
        public OrdersPage()
        {
            InitializeComponent();
            
        }
        public void Refresh()
        {
            if (Account.AuthUser.RoleId == 3)
            {
                OrderList.ItemsSource = App.db.Order.Where(z => z.UserId == Account.AuthUser.Id).ToList();
               
            }
            else
            {
                OrderList.ItemsSource = App.db.Order.ToList();
               
            }
        }
     
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           Refresh();
        }

        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            EditOrderWindow selOrder = new EditOrderWindow((sender as Button).DataContext as Order);
            selOrder.ShowDialog();
            Refresh();


        }
    }
}
