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

namespace WpfApp2TypeDiabet.Pages
{
    /// <summary>
    /// Логика взаимодействия для GuestMainPage.xaml
    /// </summary>
    public partial class GuestMainPage : Page
    {
        public GuestMainPage()
        {
            InitializeComponent();
        }

        private void ExitMenuClick(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете завершити роботу програми?",
                "Закриття додатку", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }

        }
    }
}
