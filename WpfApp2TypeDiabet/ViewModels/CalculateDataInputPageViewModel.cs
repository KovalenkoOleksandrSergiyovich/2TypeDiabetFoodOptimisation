using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class CalculateDataInputPageViewModel : BindableBase
    {
        private NavigationService _navigation;
        public string Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Gender { get; set; }
        public string PhysicalActivity { get; set; }
        public CalculateDataInputPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;
        }
        public ICommand CalculateCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save changes to user? and run calculations 
            _navigation.Navigate(new GoodsBasketPage());
        }, ()=> !string.IsNullOrEmpty(Age) && !string.IsNullOrEmpty(Height) &&
        !string.IsNullOrEmpty(Weight) && !string.IsNullOrEmpty(Gender) && !string.IsNullOrEmpty(PhysicalActivity));
        public ICommand CancelCalculationsCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати оптимізацію продуктвого кошика?",
                "Скасування реєстрації", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _navigation.Navigate(new UserMainPage());
            }
        });
    }
}
