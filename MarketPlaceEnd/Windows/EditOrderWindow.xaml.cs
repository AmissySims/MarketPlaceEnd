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

namespace MarketPlaceEnd.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        Order contextOrder;
        public EditOrderWindow(Order order)
        {
            InitializeComponent();
            StatusCb.ItemsSource = App.db.StatusOrder.ToList();
            contextOrder = order;
            DataContext = contextOrder;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) =>
        DialogResult = true;

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            if (contextOrder.StatusOrder == null)
            {
                MessageBox.Show("Выберите статус", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
