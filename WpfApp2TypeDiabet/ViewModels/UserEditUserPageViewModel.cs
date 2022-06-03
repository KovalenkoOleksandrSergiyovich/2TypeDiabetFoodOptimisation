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
    public class UserEditUserPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public UserEditUserPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;
        }
        public void SetData()
        {
            //TODO...
            //get data from user object in store and set in into proper fields
        }
        public ICommand UpdateUserDataCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save changes to user object
            _navigation.Navigate(new UserProfile());
        }, ()=>!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Gender) &&
        !string.IsNullOrEmpty(Age) && !string.IsNullOrEmpty(Height) && !string.IsNullOrEmpty(Weight));
        public ICommand CancelAnUpdateUserDataCommand => new DelegateCommand(() =>
        {
            //TODO...
            //check if there were any changes, and if there are 
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати внесення змін?",
               "Скасування внесення змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //ClearFields();
                _navigation.Navigate(new UserProfile());
            }
        });
    }
}
