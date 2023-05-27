using MarketPlaceEnd.Models;
using System.Collections.Generic;
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
        MarketPlaceEntities _context = new MarketPlaceEntities();
        public OrdersPage()
        {
            InitializeComponent();
           
            List<StatusOrder> listStatus = _context.StatusOrder.ToList();

            listStatus.Insert(0, new StatusOrder {Title = "Все"});

            StatusCb.ItemsSource = listStatus;
            StatusCb.SelectedIndex = 0;
            Sort();


        }
        public void Refresh()
        {
            if (Account.AuthUser.RoleId == 3)
            {
                OrderList.ItemsSource = App.db.Order.Where(z => z.UserId == Account.AuthUser.Id).ToList();
                Sort();
            }
            else
            {
                OrderList.ItemsSource = App.db.Order.ToList();
                Sort();
            }
        }
        private void Sort()
        {
            List<Order> listOrder = _context.Order.ToList();

            if (StatusCb.SelectedIndex != 0)
            {
                StatusOrder selOrder = (StatusOrder)StatusCb.SelectedItem;
                listOrder = listOrder.Where(x => x.StatusOrderId == selOrder.Id).ToList();
            }
            OrderList.ItemsSource = listOrder;
        }
        private void StatusCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
