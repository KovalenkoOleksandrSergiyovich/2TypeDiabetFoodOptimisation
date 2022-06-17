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

        public ObservableCollection<OptimizeService.GoodToOptimize> GoodList { get; set; } = new ObservableCollection<OptimizeService.GoodToOptimize>();
        public ObservableCollection<GoodInBasket> GoodsInBasket { get; set; } = new ObservableCollection<GoodInBasket>();
        public string Period { get; set; }
        public string TotalPrice { get; set; }
        public string TotalBU { get; set; }
        public string MaximumPrice { get; set; }
        public UserGoodsBasketPageViewModel(NavigationService navigation, UserService userService, OptimizeService optimizeService, 
            GoodInShopService goodInShopService, GoodBasketService goodBasketService)
        {
            _navigation = navigation;
            _userService = userService;
            _optimizeService = optimizeService;
            _goodInShopService = goodInShopService;
            _goodBasketService = goodBasketService;

            Period = _optimizeService.OptimizeModel.Period;
            TotalPrice = _optimizeService.OptimizeModel.Result.First().Price.ToString();
            MaximumPrice = _optimizeService.OptimizeModel.MaxSum.ToString();
            TotalBU = _optimizeService.OptimizeModel.Result.First().BU.ToString();

            foreach(var v in _optimizeService.OptimizeModel.Result)
            {
                foreach (var vv in v.ProductBasket)
                {
                    GoodToOptimize goodToOptimize = new GoodToOptimize();
                    Goods good = _goodInShopService.GetGoodByShopID(vv.GoodInShopID);
                    GoodInShop goodInShop = _goodInShopService.GetGoodInShop(vv.GoodInShopID);
                    goodToOptimize.GoodInShopID = vv.GoodInShopID;
                    goodToOptimize.GoodAmount = vv.Amount;
                    goodToOptimize.GoodName = good.GoodName;
                    goodToOptimize.GoodPrice = goodInShop.GoodPrice * vv.Amount;
                    goodToOptimize.GoodBU = _goodInShopService.GetGoodBUByShopID(vv.GoodInShopID) * vv.Amount;
                    goodToOptimize.GoodUnit = goodInShop.GoodUnits;
                    GoodList.Add(goodToOptimize);
                }
            }
        }
        public ICommand SaveBasketCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            foreach (var v in _optimizeService.OptimizeModel.Result)
            {
                foreach (var vv in v.ProductBasket)
                {
                    GoodInBasket goodInBasket = new GoodInBasket(vv.GoodInShopID, vv.Amount);
                    GoodsInBasket.Add(goodInBasket);
                }
            }
            string goodBasketCreationResult = _goodBasketService.CreateGoodBasket(_userService.CurrentUser.id, double.Parse(TotalBU, CultureInfo.InvariantCulture),
                double.Parse(TotalPrice, CultureInfo.InvariantCulture), GoodsInBasket);
            if(goodBasketCreationResult!="Success")
            {
                MessageBox.Show("Неможливо зберегти продуктовий кошик","Збереження продуктового кошику",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                _navigation.Navigate(new BasketListPage());
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
