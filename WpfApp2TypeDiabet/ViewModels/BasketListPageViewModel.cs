using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp2TypeDiabet.Models;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;
using static WpfApp2TypeDiabet.Services.OptimizeService;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class BasketListPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;
        private readonly GoodBasketService _goodBasketSercive;
        private readonly GoodInBasketService _goodInBasketService;
        private readonly GoodInShopService _goodInShopService;

        public class BasketToView
            {
                public string GoodName { get; set; }
            public double GoodAmount { get; set; }
            public double Price { get; set; }
            public double BU { get; set; }
            public string Units { get; set; }
            public int BasketNO { get; set; }

            }
        
        public ObservableCollection<GoodBasket> GoodList { get; set; } = new ObservableCollection<GoodBasket>();
        public ObservableCollection<BasketToView> GoodBaskets { get; set; } = new ObservableCollection<BasketToView>();
        public BasketListPageViewModel(NavigationService navigation, UserService userService, GoodBasketService goodBasketService, GoodInBasketService goodInBasketService,
            GoodInShopService goodInShopService)
        {
            _navigation = navigation;
            _userService = userService;
            _goodBasketSercive = goodBasketService;
            _goodInBasketService = goodInBasketService;
            _goodInShopService = goodInShopService;

            if(GoodBaskets.Any())
            {
                GoodBaskets.Clear();
            }
            if (GoodList.Any())
            {
                GoodList.Clear();
            }
            GoodList = _goodBasketSercive.GetAllBaskets(_userService.CurrentUser);
            foreach(var b in GoodList)
            {
                
                b.GoodInBasket = _goodInBasketService.GetGoodInBaskets(b);
                foreach(var g in b.GoodInBasket)
                {
                    BasketToView basket = new BasketToView();
                    basket.GoodName = _goodInShopService.GetGoodByShopID(g.GoodInShopID).GoodName;
                    basket.GoodAmount = g.Amount;
                    basket.Price = b.Price;
                    basket.BU = b.BU;
                    basket.Units = _goodInShopService.GetGoodInShop(g.GoodInShopID).GoodUnits;
                    basket.BasketNO = b.id;
                    GoodBaskets.Add(basket);
                }
            }
        }
        public ICommand GoToMainCommand => new DelegateCommand(() =>
        {
            if(_userService.CurrentUser.IsSuperUser)
            {
                _navigation.Navigate(new AdminMainPage());
            }
            else
            {
                _navigation.Navigate(new UserMainPage());
            }
        });
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            _navigation.GoBack();
        });
    }
}
