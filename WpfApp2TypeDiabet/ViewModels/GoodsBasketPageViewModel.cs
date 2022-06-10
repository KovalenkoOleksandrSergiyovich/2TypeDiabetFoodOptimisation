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
    public class GoodsBasketPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;

        public GoodsBasketPageViewModel(NavigationService navigation, UserService userService)
        {
            _navigation = navigation;
            _userService = userService;
        }
        public ICommand SaveBasketCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            _navigation.Navigate(new BasketListPage());
        });
        public ICommand ExportAsDocCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            //_navigation.Navigate(new BasketListPage());
        });
        public ICommand ExportAsXlsCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            //_navigation.Navigate(new BasketListPage());
        });
        public ICommand ExportAsPdfCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            //_navigation.Navigate(new BasketListPage());
        });
        public ICommand PrintCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            //_navigation.Navigate(new BasketListPage());
        });
        public ICommand ViewBasketsListCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            _navigation.Navigate(new BasketListPage());
        });
        public ICommand GoToThePreviousPageCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            _navigation.GoBack();
        });
        public ICommand GoToTheMainPageCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            if (_userService.CurrentUser.IsSuperUser)
            {
                _navigation.Navigate(new AdminMainPage());
            }
            else
            {
                if (_userService.CurrentUser.UserName.Equals("Guest"))
                {
                    _navigation.Navigate(new GuestMainPage());
                }
                else
                {
                    _navigation.Navigate(new UserMainPage());
                }
            }
        });
    }
}
