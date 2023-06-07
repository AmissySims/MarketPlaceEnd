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
    /// Логика взаимодействия для AddEditTypesPage.xaml
    /// </summary>
    public partial class AddEditTypesPage : Page
    {
        TypeProduct contextType;
        DbPropertyValues oldValues;
        public AddEditTypesPage(TypeProduct type)
        {
            InitializeComponent();
            contextType = type;
            DataContext = contextType;
            
            if (contextType.Id != 0)
            {
                oldValues = App.db.Entry(contextType).CurrentValues.Clone();
            }
        }

        private void SaveBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(contextType.Title))
                {
                    MessageBox.Show("Заполните поле названия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    if (contextType.Id == 0)
                    {
                        App.db.TypeProduct.Add(contextType);
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

        private void CancelBt_Click(object sender, RoutedEventArgs e)
        {
            if (oldValues != null)
            {
                App.db.Entry(contextType).CurrentValues.SetValues(oldValues);

            }
            NavigationService.GoBack();
        }

        private void TitleTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
