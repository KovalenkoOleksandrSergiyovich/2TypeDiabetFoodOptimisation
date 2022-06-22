using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2TypeDiabet.Models;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class AdminUserViewPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;
        private readonly UserGoodListService _userGoodListService;
        private readonly UserRestrictionListService _userRestrictionListService;
        private readonly GoodBasketService _goodBasketService;

        public ObservableCollection<ViewUser> UsersList { get; set; }
        public ViewUser SelectedUser { get; set; } 
        public string SelectedUserName { get; set; }
       
        public class ViewUser
        {
            public string UserName { get; set; }
            public int UserGoodsCount { get; set; }
            public int UserRestrictionsCount { get; set; }
            public int UserBasketsCount { get; set; }
            public ViewUser(string userName, int userGoodsCount, int userRestrictionsCount, int userBasketsCount)
            {
                UserName = userName;
                UserGoodsCount = userGoodsCount;
                UserRestrictionsCount = userRestrictionsCount;
                UserBasketsCount = userBasketsCount;
            }
        }

        public AdminUserViewPageViewModel(NavigationService navigation, UserService userService, UserGoodListService userGoodListService,
            UserRestrictionListService userRestrictionListService, GoodBasketService goodBasketService)
        {
            _navigation = navigation;
            _userService = userService;
            _userGoodListService = userGoodListService;
            _userRestrictionListService = userRestrictionListService;
            _goodBasketService = goodBasketService;
            FillUsersList();
        }
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            _navigation.GoBack();
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new AdminMainPage());
        });
        public ICommand DeleteUserCommand => new DelegateCommand(() =>
        {
            MessageBoxResult boxResult = MessageBox.Show("Ви впевнені, що хочете видалити користувача "+SelectedUser.UserName+" ?",
                "Видалення користувача", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(boxResult==MessageBoxResult.Yes)
            {
                User userToDelete = _userService.GetUser(SelectedUser.UserName);
                string result = _userService.DeleteUser(userToDelete);
                if (!result.Equals("Success"))
                {
                    MessageBox.Show("Помилка при видаленні користувача " + SelectedUser.UserName, "Видалення користувача", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Користувача " + SelectedUser.UserName + " було успішно видалено", "Видалення користувача", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillUsersList();
                }
            }
        });
        public void FillUsersList()
        {
            UsersList = new ObservableCollection<ViewUser>();
            
            var users = _userService.GetUsersList(_userService.CurrentUser);
            foreach(var u in users)
            {
                var userGoodsCount = _userGoodListService.GetUserGoods(u).Count();
                var userRestrictionsCount = _userRestrictionListService.GetUserRestrictions(u).Count();
                var goodBasketsCount = _goodBasketService.GetAllBaskets(u).Count();
                ViewUser viewUser = new(u.UserName, userGoodsCount, userRestrictionsCount, goodBasketsCount);
                UsersList.Add(viewUser);
            }
        }
    }
}
