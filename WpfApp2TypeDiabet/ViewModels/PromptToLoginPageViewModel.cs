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
    public class PromptToLoginPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;

        public PromptToLoginPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;
        }
        public ICommand ToLoginCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new LoginPage());
        });
        public ICommand ContinueAsGuestCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GuestMainPage());
        });
    }
}
