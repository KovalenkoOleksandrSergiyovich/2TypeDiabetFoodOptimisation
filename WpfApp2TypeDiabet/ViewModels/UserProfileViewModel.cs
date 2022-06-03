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
    public class UserProfileViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        public UserProfileViewModel(NavigationService navigation)
        {
            _navigation = navigation;
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
                _navigation.Navigate(new PromptToLogin());
            }
        });
    }
}
