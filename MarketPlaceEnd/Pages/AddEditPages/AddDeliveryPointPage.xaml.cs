using MarketPlaceEnd.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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

namespace MarketPlaceEnd.Pages.AddEditPages
{
    /// <summary>
    /// Логика взаимодействия для AddDeliveryPointPage.xaml
    /// </summary>
    public partial class AddDeliveryPointPage : Page
    {
        DeliveryPoint contextPoint;
        DbPropertyValues oldValues;
        public AddDeliveryPointPage(DeliveryPoint point)
        {
            InitializeComponent();
            var users = App.db.User.Where(x => x.RoleId == 5).ToList();
            RoleCb.ItemsSource = users;
            contextPoint = point;
            DataContext = contextPoint;

            if (contextPoint.Id != 0)
            {
                oldValues = App.db.Entry(contextPoint).CurrentValues.Clone();
            }
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contextPoint.Adress))
            {
                MessageBox.Show("Заполните поле адреса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (contextPoint.User == null)
            {
                MessageBox.Show("Выберите работника", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                if (contextPoint.Id == 0)
                {
                    App.db.DeliveryPoint.Add(contextPoint);
                }
                App.db.SaveChanges();
                MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            if (oldValues != null)
            {
                App.db.Entry(contextPoint).CurrentValues.SetValues(oldValues);

            }
            NavigationService.GoBack();
        }
    }
}
