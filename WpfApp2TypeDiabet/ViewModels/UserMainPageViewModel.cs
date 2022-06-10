﻿using DevExpress.Mvvm;
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
    public class UserMainPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;
        public string UserName { get; set; }
        public UserMainPageViewModel(NavigationService navigation, UserService userService)
        {
            _navigation = navigation;
            _userService = userService;
            UserName = _userService.CurrentUser.UserName;
        }
        public ICommand StandartGoodsListCommand => new DelegateCommand(() =>
        {
            //TODO...
            //create list of default goods and put in into store
            _navigation.Navigate(new GoodsViewPage());
        });
        public ICommand CustomGoodsListCommand => new DelegateCommand(() =>
        {
            //TODO...
            //create list of custom user goods and put in into store
            _navigation.Navigate(new GoodsViewPage());
        });
        public ICommand StandartRestrictionsListCommand => new DelegateCommand(() =>
        {
            //TODO...
            //create list of default restrictions and default goods and put in into stores
            _navigation.Navigate(new RestrictionPage());
        });
        public ICommand CustomRestrictionsListCommand => new DelegateCommand(() =>
        {
            //TODO...
            //create list of custom restrictions and custom goods and put in into stores
            _navigation.Navigate(new RestrictionPage());
        });
        public ICommand OptimizeFoodConsumingCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new CalculateDataInputPage());
        });
        public ICommand ViewGoodBacketsListCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new BasketListPage());
        });
        public ICommand ViewUserProfileCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new UserProfile());
        });
        public ICommand UserLogOutCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете вийти з облікового запису?",
                "Вихід з облікового запису", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //TODO
                //clear User store from current user
                _userService.LogOut();
                _navigation.Navigate(new PromptToLogin());
            }
        });

        public ICommand ExitCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете завершити роботу програми?",
                "Закриття додатку", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                //TODO...
                //LogOut user and clear user store
                _userService.LogOut();
                Application.Current.Shutdown();
            }
        });
    }
}
