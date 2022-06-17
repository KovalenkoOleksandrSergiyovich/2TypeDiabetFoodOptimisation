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
using static WpfApp2TypeDiabet.Services.OptimizeService;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class UserGoodsBasketPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;
        private readonly OptimizeService _optimizeService;

        public ObservableCollection<ProductBasket> GoodList { get; set; } = new ObservableCollection<OptimizeService.ProductBasket>();
        public string Period { get; set; }
        public string TotalPrice { get; set; }
        public string TotalBU { get; set; }
        public string MaximumPrice { get; set; }
        public UserGoodsBasketPageViewModel(NavigationService navigation, UserService userService, OptimizeService optimizeService)
        {
            _navigation = navigation;
            _userService = userService;
            _optimizeService = optimizeService;

            Period = _optimizeService.OptimizeModel.Period;
            TotalPrice = _optimizeService.OptimizeModel.Result.First().Price.ToString();
            MaximumPrice = _optimizeService.OptimizeModel.MaxSum.ToString();
            TotalBU = _optimizeService.OptimizeModel.Result.First().BU.ToString();

            foreach(var v in _optimizeService.OptimizeModel.Result)
            {
                foreach (var vv in v.ProductBasket)
                {
                    GoodList.Add(vv);
                }
            }
        }
        public ICommand SaveBasketCommand => new DelegateCommand(() =>
        {
            //TODO...
            //save current record to the list
            _navigation.Navigate(new BasketListPage());
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
