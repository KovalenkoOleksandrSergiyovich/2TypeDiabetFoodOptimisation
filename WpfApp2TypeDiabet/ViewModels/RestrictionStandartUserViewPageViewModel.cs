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
    public class RestrictionStandartUserViewPageViewModel : BindableBase
    {
        private NavigationService _navigation;
        private RestrictionService _restrictionService;
        private readonly UserService _userService;

        public ObservableCollection<UserGoodRestriction> UserRestrictionsList { get; set; } = new ObservableCollection<RestrictionService.UserGoodRestriction>();
        public RestrictionStandartUserViewPageViewModel(NavigationService navigation, RestrictionService restrictionService, UserService userService)
        {
            _navigation = navigation;
            _restrictionService = restrictionService;
            _userService = userService;
            FillRestrictionList();
        }
        public ICommand GoBackCommand => new DelegateCommand(()=>
        {
            ClearList();
            _navigation.GoBack();
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
            ClearList();
            _navigation.Navigate(new UserMainPage());
        });
        public void FillRestrictionList()
        {
            if (UserRestrictionsList.Any())
            {
                UserRestrictionsList.Clear();
            }
            UserRestrictionsList = _restrictionService.GetStandartGoodRestrictions();
        }
        public void ClearList()
        {
            UserRestrictionsList.Clear();
        }
    }
}
