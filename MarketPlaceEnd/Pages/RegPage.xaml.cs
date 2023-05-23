using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MarketPlaceEnd.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void RegBt_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Регистрация выполнена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void EnterBt_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }
    }
}
