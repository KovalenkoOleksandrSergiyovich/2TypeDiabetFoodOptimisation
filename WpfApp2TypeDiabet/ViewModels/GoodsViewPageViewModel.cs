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
    public class GoodsViewPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        public GoodsViewPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;
        }
        public ICommand GoToGoodPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GoodPage("Додавання нового товару"));
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GuestMainPage());
        });
    }
}
