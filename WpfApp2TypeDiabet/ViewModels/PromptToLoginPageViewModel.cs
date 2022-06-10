using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp2TypeDiabet.Models;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class PromptToLoginPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;

        public PromptToLoginPageViewModel(NavigationService navigation, UserService userService)
        {
            _navigation = navigation;
            _userService = userService;
        }
        public ICommand ToLoginCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new LoginPage());
        });
        public ICommand ContinueAsGuestCommand => new DelegateCommand(() =>
        {
            string UserName = "Guest";
            int Age = 23;
            double Height = 1.95;
            double Weight = 104.65;
            string Gender = "Чоловіча";
            User newUser = new User(UserName, null, Age, Height, Weight, Gender, false);
            _userService.CurrentUser = newUser;
            _navigation.Navigate(new GuestMainPage());
        });
    }
}
