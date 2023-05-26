using MarketPlaceEnd.Models;
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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        MarketPlaceEntities _context = new MarketPlaceEntities();
        public OrdersPage()
        {
            InitializeComponent();
            if(Account.AuthUser.RoleId == 3)
            {
                OrderList.ItemsSource = App.db.Order.Where(z => z.UserId == Account.AuthUser.Id).ToList();
            }
            else
            {
                OrderList.ItemsSource = App.db.Order.ToList();
            }
            Sort();
           
        }

        private void Sort()
        {
            List<Order> listOrder = _context.Order.ToList();

            if (StatusCb.SelectedIndex != 0)
            {
                StatusOrder selOrder = (StatusOrder)StatusCb.SelectedItem;
                listOrder = listOrder.Where(x => x.StatusOrderId == selOrder.Id).ToList();
            }
        }
        private void StatusCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

      
    }
}
