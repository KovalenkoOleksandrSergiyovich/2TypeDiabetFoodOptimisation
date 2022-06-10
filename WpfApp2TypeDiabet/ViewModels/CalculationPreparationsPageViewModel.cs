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
    public class CalculationPreparationsPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;

        public CalculationPreparationsPageViewModel(NavigationService navigation, UserService userService)
        {
            _navigation = navigation;
            _userService = userService;
        }
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати оптимізацію продуктвого кошика?",
                "Скасування оптимізації", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _navigation.GoBack();
            }
        });
        public ICommand GoToMainCommand => new DelegateCommand(() =>
        {
        MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати обчислення?",
            "Скасування обчислень", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (_userService.CurrentUser.IsSuperUser)
                {
                    _navigation.Navigate(new AdminMainPage());
                }
                else
                {
                    if(_userService.CurrentUser.UserName.Equals("Guest"))
                    {
                        _navigation.Navigate(new GuestMainPage());
                    }
                    else
                    {
                        _navigation.Navigate(new UserMainPage());
                    }
                }
            }
        });
    }
}
