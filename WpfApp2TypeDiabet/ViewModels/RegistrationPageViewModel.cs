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
    public class RegistrationPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public RegistrationPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;
        }
        private void ClearFields()
        {
            if (!string.IsNullOrEmpty(UserName))
            {
                UserName = string.Empty;
            }
            if (!string.IsNullOrEmpty(Password))
            {
                Password = string.Empty;
            }
            if(!string.IsNullOrEmpty(Gender))
            {
                Gender = string.Empty;
            }
            if(!string.IsNullOrEmpty(Age))
            {
                Age = string.Empty;
            }
            if(!string.IsNullOrEmpty(Height))
            {
                Height = string.Empty;
            }
            if(!string.IsNullOrEmpty(Weight))
            {
                Weight = string.Empty;
            }
        }
        public ICommand GoToLoginPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new LoginPage());
            ClearFields();
        });
        public ICommand RegisterUserCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new UserMainPage());
            //create user
            //TODO....
            ClearFields();
        }, () => !string.IsNullOrWhiteSpace(UserName) && 
        !string.IsNullOrWhiteSpace(Password) &&
        !string.IsNullOrWhiteSpace(Age) &&
        !string.IsNullOrWhiteSpace(Height) &&
        !string.IsNullOrWhiteSpace(Gender) &&
        !string.IsNullOrWhiteSpace(Weight));
        public ICommand CancelRegistrationCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new PromptToLogin());
            ClearFields();
        });
    }
}
