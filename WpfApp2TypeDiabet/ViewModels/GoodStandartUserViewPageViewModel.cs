﻿using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;
using static WpfApp2TypeDiabet.Services.UserGoodListService;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class GoodStandartUserViewPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserGoodListService _userGoodListService;

        public ObservableCollection<UserGood> UserGoodsList { get; set; } = new ObservableCollection<UserGoodListService.UserGood>();
        public UserGood SelectedUserGood { get; set; }

        public GoodStandartUserViewPageViewModel(NavigationService navigation, UserGoodListService userGoodListService)
        {
            _navigation = navigation;
            _userGoodListService = userGoodListService;
            if (UserGoodsList.Any())
            {
                UserGoodsList.Clear();
            }
            UserGoodsList = _userGoodListService.GetStandartGoods();

        }
        public ICommand GoBackCommand => new DelegateCommand(()=>
        {
            _navigation.GoBack();
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(()=>
        {
            _navigation.Navigate(new UserMainPage());
        });
    }
}
