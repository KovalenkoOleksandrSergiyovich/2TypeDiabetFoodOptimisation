using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class GuestMainPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;

        public GuestMainPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;
        }
        public ICommand GuestGoodsListCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GoodsViewPage());
        });
        public ICommand RestrictionsListCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new RestrictionsPage());
        });
        public ICommand CalculationDataInputCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new CalculateDataInputPage());
        });
    }
}
