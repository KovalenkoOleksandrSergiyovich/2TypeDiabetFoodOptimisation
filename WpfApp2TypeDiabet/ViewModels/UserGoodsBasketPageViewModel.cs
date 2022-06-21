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
using WpfApp2TypeDiabet.Models;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;
using static WpfApp2TypeDiabet.Services.OptimizeService;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class UserGoodsBasketPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;
        private readonly OptimizeService _optimizeService;
        private readonly GoodInShopService _goodInShopService;
        private readonly GoodBasketService _goodBasketService;
        private readonly GoodInBasketService _goodInBasketService;

        public ObservableCollection<OptimizeService.GoodToOptimize> GoodList { get; set; } = new ObservableCollection<OptimizeService.GoodToOptimize>();
        public ObservableCollection<GoodInBasket> GoodsInBasket { get; set; } = new ObservableCollection<GoodInBasket>();
        public string Period { get; set; }
        public double TotalPrice { get; set; }
        public double TotalBU { get; set; }
        public double MaximumPrice { get; set; }
        public UserGoodsBasketPageViewModel(NavigationService navigation, UserService userService, OptimizeService optimizeService, 
            GoodInShopService goodInShopService, GoodBasketService goodBasketService, GoodInBasketService goodInBasketService)
        {
            _navigation = navigation;
            _userService = userService;
            _optimizeService = optimizeService;
            _goodInShopService = goodInShopService;
            _goodBasketService = goodBasketService;
            _goodInBasketService = goodInBasketService;

            Period = _optimizeService.OptimizeModel.Period;
            TotalPrice = _optimizeService.OptimizeModel.Result.First().Price;
            MaximumPrice = _optimizeService.OptimizeModel.MaxSum;
            TotalBU = _optimizeService.OptimizeModel.Result.First().BU;

            foreach(var v in _optimizeService.OptimizeModel.Result)
            {
                foreach (var vv in v.ProductBasket)
                {
                    GoodToOptimize goodToOptimize = new GoodToOptimize();
                    Goods good = _goodInShopService.GetGoodByShopID(vv.GoodInShopID);
                    GoodInShop goodInShop = _goodInShopService.GetGoodInShop(vv.GoodInShopID);
                    goodToOptimize.GoodInShopID = vv.GoodInShopID;
                    goodToOptimize.GoodUnit = goodInShop.GoodUnits;
                    if(goodToOptimize.GoodUnit.Equals("кілограми") || goodToOptimize.GoodUnit.Equals("літри"))
                    {
                        goodToOptimize.GoodAmount = Math.Round(vv.Amount/1000,4);
                    }
                    else
                    {
                        goodToOptimize.GoodAmount = Math.Round(vv.Amount,4);
                    }
                    goodToOptimize.GoodName = good.GoodName;
                    goodToOptimize.GoodPrice = Math.Round(goodInShop.GoodPrice/goodInShop.GoodAmount * vv.Amount,4);
                    goodToOptimize.GoodBU = Math.Round(_goodInShopService.GetGoodBUByShopID(vv.GoodInShopID) * vv.Amount,4);
                    
                    GoodList.Add(goodToOptimize);
                }
            }
        }
        public ICommand SaveBasketCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            string goodBasketCreationResult = _goodBasketService.CreateGoodBasket(_userService.CurrentUser.id, TotalBU,
                TotalPrice);
            if(goodBasketCreationResult!="Success")
            {
                MessageBox.Show("Неможливо зберегти продуктовий кошик","Збереження продуктового кошику",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(GoodsInBasket.Any())
                {
                    GoodsInBasket.Clear();
                }
                foreach (var v in _optimizeService.OptimizeModel.Result)
                {
                    foreach (var vv in v.ProductBasket)
                    {
                        GoodInBasket goodInBasket = new GoodInBasket(vv.GoodInShopID, vv.Amount);
                        goodInBasket.GoodBasketID = _goodBasketService.GoodBasket.id;
                        GoodsInBasket.Add(goodInBasket);
                    }
                    string goodInBasketCreationResult = _goodInBasketService.CreateGoodInBasket(GoodsInBasket);
                    if (goodInBasketCreationResult != "Success")
                    {
                        MessageBox.Show("Неможливо зберегти продукт в кошику", "Збереження продуктового кошику", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        _goodBasketService.GoodBasket.GoodInBasket = GoodsInBasket;
                        _navigation.Navigate(new BasketListPage());
                    }
                }
            }
            
        });
        public ICommand ExportAsDocCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            //_navigation.Navigate(new BasketListPage());
        });
        public ICommand ExportAsXlsCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            //_navigation.Navigate(new BasketListPage());
        });
        public ICommand ExportAsPdfCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            //_navigation.Navigate(new BasketListPage());
        });
        public ICommand PrintCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            //_navigation.Navigate(new BasketListPage());
        });
        public ICommand ViewBasketsListCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            _navigation.Navigate(new BasketListPage());
        });
        public ICommand GoToThePreviousPageCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            _navigation.GoBack();
        });
        public ICommand GoToTheMainPageCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            if (_userService.CurrentUser.IsSuperUser)
            {
                _navigation.Navigate(new AdminMainPage());
            }
            else
            {
                if (_userService.CurrentUser.UserName.Equals("Guest"))
                {
                    _navigation.Navigate(new GuestMainPage());
                }
                else
                {
                    _navigation.Navigate(new UserMainPage());
                }
            }
        });
    }
}
