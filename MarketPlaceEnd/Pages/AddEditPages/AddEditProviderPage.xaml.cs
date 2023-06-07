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
    /// Логика взаимодействия для AddEditProviderPage.xaml
    /// </summary>
    public partial class AddEditProviderPage : Page
    {
        Provider contextProv;
        DbPropertyValues oldValues;
        public AddEditProviderPage(Provider prov)
        {
            InitializeComponent();
            contextProv = prov;
            DataContext = contextProv;

            if (contextProv.Id != 0)
            {
                oldValues = App.db.Entry(contextProv).CurrentValues.Clone();
            }
        }

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            if (oldValues != null)
            {
                App.db.Entry(contextProv).CurrentValues.SetValues(oldValues);

            }
            NavigationService.GoBack();
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(contextProv.Title))
                {
                    MessageBox.Show("Заполните поле названия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(contextProv.Adress))
                {
                    MessageBox.Show("Заполните поле адреса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (contextProv.Id == 0)
                    {
                        App.db.Provider.Add(contextProv);
                    }
                    App.db.SaveChanges();
                    MessageBox.Show("Сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
