using DevExpress.Mvvm;
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
        //властивість для отримання та встановлення значень логіну користувача
        public string Login {get; set;}
        //властивість для отримання та встановлення значень паролю користувача
        public string Password {get; set;}
        //конструктор класу
        public LoginPageViewModel(NavigationService navigation, UserService userSevice)
        {
            _navigation = navigation;
            _userService = userSevice;
        }
        //команда переходу на форму авторизації
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
        //команда переходу на форму запиту на авторизацію
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
        //команда авторизації
        public ICommand LoginCommand => new DelegateCommand(() =>
        {
            User userToLogin = _userService.AttemptToLogin(Login, Password);
            if (userToLogin != null)
            {
                _userService.CurrentUser = userToLogin;
                if (userToLogin.IsSuperUser)
                {
                    _navigation.Navigate(new AdminMainPage());
                }
                else
                {
                    _navigation.Navigate(new UserMainPage());
                }
                ClearFields();  
            }
            else
            {
                MessageBox.Show("Неможливо авторизуватися!", "Помилка авторизації", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }, ()=>!string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password));
        //метод очищення полів
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
