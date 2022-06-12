using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;
using static WpfApp2TypeDiabet.Services.RestrictionService;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class RestrictionCustomUserViewPageViewModel : BindableBase
    {
        private NavigationService _navigation;
        private RestrictionService _restrictionService;
        private readonly UserService _userService;

        public ObservableCollection<UserRestriction> UserRestrictionsList { get; set; } = new ObservableCollection<RestrictionService.UserRestriction>();
        public UserRestriction SelectedUserRestriction { get; set; }
        public RestrictionCustomUserViewPageViewModel(NavigationService navigation, RestrictionService restrictionService, UserService userService)
        {
            _navigation = navigation;
            _restrictionService = restrictionService;
            _userService = userService;
            FillRestrictionList();
        }
        public ICommand GoBackCommand => new DelegateCommand(()=>
        {
            _navigation.GoBack();
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new UserMainPage());
        });
        public ICommand GoToRestrictionPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new RestrictionUserAddPage());
        });
        public void FillRestrictionList()
        {
            if(UserRestrictionsList.Any())
            {
                UserRestrictionsList.Clear();
            }
            UserRestrictionsList = _restrictionService.GetUserRestrictions(_userService.CurrentUser);
        }
    }
}
