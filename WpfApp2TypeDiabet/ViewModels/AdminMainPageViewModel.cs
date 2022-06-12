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
    public class AdminMainPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;
        public string UserName { get; set; }
        public AdminMainPageViewModel(NavigationService navigationService, UserService userService)
        {
            _navigation = navigationService;
            _userService = userService;
            UserName = _userService.CurrentUser.UserName;
        }
        public ICommand ViewUsersListCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new AdminUserViewPage());
        });
        public ICommand AdminAddGoodCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GoodAddAdminPage());
        });
        public ICommand ViewGoodsListCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GoodViewAdminPage());
        });
        public ICommand AdminAddRestrictionCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new RestrictionAddAdminPage());
        });
        public ICommand ViewRestrictionsListCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new RestrictionViewAdminPage());
        });
        public ICommand OptimizeFoodConsumingCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new CalculateDataInputPage());
        });
        public ICommand ViewUserProfileCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new UserProfile());
        });
        public ICommand ViewGoodBacketsListCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new BasketListPage());
        });
        public ICommand UserLogOutCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете вийти з облікового запису?",
                "Вихід з облікового запису", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //TODO
                //clear User store from current user
                _userService.LogOut();
                _navigation.Navigate(new PromptToLogin());
            }
        });

        public ICommand ExitCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете завершити роботу програми?",
                "Закриття додатку", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //TODO...
                //LogOut user and clear user store
                _userService.LogOut();
                Application.Current.Shutdown();
            }
        });
    }
}
