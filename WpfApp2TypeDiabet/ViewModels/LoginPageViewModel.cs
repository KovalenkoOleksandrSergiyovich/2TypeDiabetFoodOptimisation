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
    public class LoginPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;

        public string Login {get; set;}
        public string Password {get; set;}
        public LoginPageViewModel(NavigationService navigation, UserService userSevice)
        {
            _navigation = navigation;
            _userService = userSevice;
        }
        public ICommand GoToRegistrationPageCommand => new DelegateCommand(() =>
        {
            if (!string.IsNullOrEmpty(Login) || !string.IsNullOrEmpty(Password))
            {
                MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати авторизацію?",
                "Скасування авторизації", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ClearFields();
                    _navigation.Navigate(new RegistrationPage());
                }
            }
            else
            {
                ClearFields();
                _navigation.Navigate(new RegistrationPage());
            }
        });
        public ICommand GoToPromptPageCommand => new DelegateCommand(() =>
        {
            if (!string.IsNullOrEmpty(Login) || !string.IsNullOrEmpty(Password))
            {
                MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати авторизацію?",
                "Скасування авторизації", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ClearFields();
                    _navigation.Navigate(new PromptToLogin());
                }
            }
            else
            {
                ClearFields();
                _navigation.Navigate(new PromptToLogin());
            }
        });
        public ICommand LoginCommand => new DelegateCommand(() =>
        {
            User userToLogin = _userService.AttemptToLogin(Login, Password);
            if (userToLogin != null)
            {
                _navigation.Navigate(new UserMainPage());
                _userService.CurrentUser = userToLogin;
                ClearFields();
            }
            else
            {
                MessageBox.Show("Неможливо авторизуватися!", "Помилка авторизації", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }, ()=>!string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password));
        private void ClearFields()
        {
            if(!string.IsNullOrEmpty(Login))
            {
                Login = string.Empty;
            }
            if(!string.IsNullOrEmpty(Password))
            {
                Password = string.Empty;
            }
        }
    }
}
