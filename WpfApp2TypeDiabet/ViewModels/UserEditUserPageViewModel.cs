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
        private readonly UserService _userService;
        public List<string> Genders { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public UserEditUserPageViewModel(NavigationService navigation, UserService userService)
        {
            _navigation = navigation;
            _userService = userService;
            Genders = new List<string>
            {
                "Чоловіча",
                "Жіноча"
            };
            SetData();
        }
        public void SetData()
        {
            //TODO...
            //get data from user object in store and set in into proper fields
            UserName = _userService.CurrentUser.UserName;
            Password = _userService.CurrentUser.Password;
            Age = _userService.CurrentUser.Age.ToString();
            Height = _userService.CurrentUser.Height.ToString();
            Weight = _userService.CurrentUser.Weight.ToString();
            Gender = _userService.CurrentUser.Gender;
        }
        public ICommand UpdateUserDataCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save changes to user object
            string message = _userService.UpdateInfo(UserName, Password, int.Parse(Age), double.Parse(Height), double.Parse(Weight), Gender);
            if (message.Equals("Success"))
            {
                MessageBox.Show("Дані користувача було упісшно оновлено", "Оновлення даних", MessageBoxButton.OK, MessageBoxImage.Information);
                //_navigation.GoBack();
                ClearFields();
                _navigation.Navigate(new UserProfile());
            }
            else
            {
                MessageBox.Show("Помилка оновлення даних: " + message, "Оновлення даних", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }, () => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Gender) &&
        !string.IsNullOrEmpty(Age.ToString()) && !string.IsNullOrEmpty(Height.ToString()) && !string.IsNullOrEmpty(Weight.ToString()));
        public ICommand CancelAnUpdateUserDataCommand => new DelegateCommand(() =>
        {
            //TODO...
            //check if there were any changes, and if there are 
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати внесення змін?",
               "Скасування внесення змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearFields();
                _navigation.GoBack();
            }
        });
        public ICommand GoToMainCommand => new DelegateCommand(() =>
        {
            //TODO...
            //check if there were any changes, and if there are 
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати внесення змін?",
               "Скасування внесення змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearFields();
                _navigation.Navigate(new UserMainPage());
            }
        });
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            //TODO...
            //check if there were any changes, and if there are 
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати внесення змін?",
               "Скасування внесення змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearFields();
                _navigation.GoBack();
            }
        });
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
            if (!string.IsNullOrEmpty(Gender))
            {
                Gender = string.Empty;
            }
            if (!string.IsNullOrEmpty(Age))
            {
                Age = string.Empty;
            }
            if (!string.IsNullOrEmpty(Height))
            {
                Height = string.Empty;
            }
            if (!string.IsNullOrEmpty(Weight))
            {
                Weight = string.Empty;
            }
        }
    }
}
