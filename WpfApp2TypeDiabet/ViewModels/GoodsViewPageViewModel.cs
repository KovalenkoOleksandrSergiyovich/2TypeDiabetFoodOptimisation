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
        private readonly UserService _userService;

        public GoodsViewPageViewModel(NavigationService navigation, UserService userService)
        {
            _navigation = navigation;
            _userService = userService;
        }
        public ICommand GoToGoodPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GoodPage("Додавання нового товару"));
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
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
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            _navigation.GoBack();
        });
    }
}
