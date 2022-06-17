using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class CalculateDataInputPageViewModel : BindableBase
    {
        private NavigationService _navigation;
        private readonly UserService _userService;
        private readonly OptimizeService _optimizeService;

        public List<string> Genders { get; set; }
        public List<string> PhysicalActivities { get; set; }
        public string Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Gender { get; set; }
        public string PhysicalActivity { get; set; }
        public CalculateDataInputPageViewModel(NavigationService navigation, UserService userService, OptimizeService optimizeService)
        {
            _navigation = navigation;
            _userService = userService;
            _optimizeService = optimizeService;

            Age = _userService.CurrentUser.Age.ToString();
            Height = _userService.CurrentUser.Height.ToString();
            Weight = _userService.CurrentUser.Weight.ToString();
            Gender = _userService.CurrentUser.Gender;
            Genders = new List<string>
            {
                "Чоловіча",
                "Жіноча"
            };
            PhysicalActivities = new List<string>
            {
                "Низький",
                "Середній",
                "Важкий"
            };

        }
        public ICommand CalculateCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save changes to user? and run calculations 
            _userService.CurrentUser.Gender = Gender;
            _userService.CurrentUser.Age = int.Parse(Age);
            _userService.CurrentUser.Height = double.Parse(Height, CultureInfo.InvariantCulture);
            _userService.CurrentUser.Weight = double.Parse(Weight, CultureInfo.InvariantCulture);
            _optimizeService.CreateModel(PhysicalActivity, _userService.CurrentUser);
            _navigation.Navigate(new CalculationPreparationsPage());
        }, ()=> !string.IsNullOrEmpty(Age) && !string.IsNullOrEmpty(Height) &&
        !string.IsNullOrEmpty(Weight) && !string.IsNullOrEmpty(Gender) && !string.IsNullOrEmpty(PhysicalActivity));
        public ICommand CancelCalculationsCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати оптимізацію продуктвого кошика?",
                "Скасування оптимізації", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (_userService.CurrentUser.IsSuperUser)
                {
                    ClearFields();
                    _navigation.Navigate(new AdminMainPage());
                }
                else
                {
                    if(_userService.CurrentUser.UserName.Equals("Guest"))
                    {
                        ClearFields();
                        _navigation.Navigate(new GuestMainPage());
                    }
                    else
                    {
                        ClearFields();
                        _navigation.Navigate(new UserMainPage());
                    }
                }
            }
        });
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати оптимізацію продуктвого кошика?",
                "Скасування оптимізації", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearFields();
                _navigation.GoBack();
            }
        });
        private void ClearFields()
        {
            if(!string.IsNullOrEmpty(Age) || !string.IsNullOrWhiteSpace(Age))
            {
                Age = string.Empty;
            }
            if (!string.IsNullOrEmpty(Height) || !string.IsNullOrWhiteSpace(Height))
            {
                Height = string.Empty;
            }
            if (!string.IsNullOrEmpty(Weight) || !string.IsNullOrWhiteSpace(Weight))
            {
                Weight = string.Empty;
            }
            if (!string.IsNullOrEmpty(Gender) || !string.IsNullOrWhiteSpace(Gender))
            {
                Gender = string.Empty;
            }
            if (!string.IsNullOrEmpty(PhysicalActivity) || !string.IsNullOrWhiteSpace(PhysicalActivity))
            {
                PhysicalActivity = string.Empty;
            }
        }
    }
}
