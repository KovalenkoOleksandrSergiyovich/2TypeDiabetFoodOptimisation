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
    public class GoodViewAdminPageViewModel : BindableBase
    {
        private NavigationService _navigation;

        public GoodViewAdminPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;
        }
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
           _navigation.Navigate(new AdminMainPage());
        });
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            _navigation.GoBack();
        });
        public ICommand GoToGoodPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GoodAddAdminPage());
        });
    }
}
