using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2TypeDiabet.Models;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class UserProfileViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;
        public string UserName { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string Gender { get; set; }
        public UserProfileViewModel(NavigationService navigation, UserService userService)
        {
            _navigation = navigation;
            _userService = userService;
            UserName = _userService.CurrentUser.UserName;
            Age = _userService.CurrentUser.Age;
            Height = _userService.CurrentUser.Height;
            Weight = _userService.CurrentUser.Weight;
            Gender = _userService.CurrentUser.Gender;
        }
        public ICommand EditUserDataCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new UserEditUserPage());
        });
        public ICommand GoToUserMainPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new UserMainPage());
        });
        public ICommand LogoutCommand => new DelegateCommand(() =>
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
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            _navigation.GoBack();
        });
        public ICommand GoToMainCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new UserMainPage());
        });
    }
}
