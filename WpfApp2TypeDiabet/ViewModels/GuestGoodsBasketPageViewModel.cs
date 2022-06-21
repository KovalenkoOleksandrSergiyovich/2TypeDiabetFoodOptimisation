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
    public class GuestGoodsBasketPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;
        private readonly OptimizeService _optimizeService;
        private readonly GoodInShopService _goodInShopService;
        private readonly GoodBasketService _goodBasketService;
        private readonly GoodInBasketService _goodInBasketService;

        public ObservableCollection<GoodToOptimize> GoodList { get; set; } = new ObservableCollection<GoodToOptimize>();
        public string Period { get; set; }
        public double TotalPrice { get; set; }
        public double TotalBU { get; set; }
        public double MaximumPrice { get; set; }
        public GuestGoodsBasketPageViewModel(NavigationService navigation, UserService userService, OptimizeService optimizeService,
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

            foreach (var v in _optimizeService.OptimizeModel.Result)
            {
                foreach (var vv in v.ProductBasket)
                {
                    GoodToOptimize goodToOptimize = new GoodToOptimize();
                    Goods good = _goodInShopService.GetGoodByShopID(vv.GoodInShopID);
                    GoodInShop goodInShop = _goodInShopService.GetGoodInShop(vv.GoodInShopID);
                    goodToOptimize.GoodInShopID = vv.GoodInShopID;
                    goodToOptimize.GoodUnit = goodInShop.GoodUnits;
                    if (goodToOptimize.GoodUnit.Equals("кілограми") || goodToOptimize.GoodUnit.Equals("літри"))
                    {
                        goodToOptimize.GoodAmount = Math.Round(vv.Amount / 1000, 4);
                    }
                    else
                    {
                        goodToOptimize.GoodAmount = Math.Round(vv.Amount, 4);
                    }
                    goodToOptimize.GoodName = good.GoodName;
                    goodToOptimize.GoodPrice = Math.Round(goodInShop.GoodPrice / goodInShop.GoodAmount * vv.Amount, 4);
                    goodToOptimize.GoodBU = Math.Round(_goodInShopService.GetGoodBUByShopID(vv.GoodInShopID) * vv.Amount, 4);

                    GoodList.Add(goodToOptimize);
                }
            }
        }
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
            _navigation.Navigate(new GuestMainPage());
        });
    }
}