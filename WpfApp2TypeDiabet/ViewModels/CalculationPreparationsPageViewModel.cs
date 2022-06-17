using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2TypeDiabet.Models;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;
using static WpfApp2TypeDiabet.Services.OptimizeService;
using static WpfApp2TypeDiabet.Services.UserGoodListService;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class CalculationPreparationsPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly UserService _userService;
        private readonly OptimizeService _optimizeService;
        private readonly UserGoodListService _userGoodListService;
        private readonly RestrictionService _restrictionService;
        private UserGoodListService.GoodToOptimize _selectedGood;
        public List<string> PeriodList { get; set; }
        public string Period { get; set; }
        public string MaximumGoodPrice { get; set; }
        public ObservableCollection<UserGoodListService.GoodToOptimize> GoodList { get; set; }
        public UserGoodListService.GoodToOptimize SelectedGood { get; set; }
        public UserGoodListService.GoodToOptimize ConfirmedSelectedGood { get; set; }
        public ObservableCollection<UserGoodListService.GoodToOptimize> SelectedGoods { get; set; } = new ObservableCollection<UserGoodListService.GoodToOptimize>();
        public CalculationPreparationsPageViewModel(NavigationService navigation, UserService userService, OptimizeService optimizeService,
            UserGoodListService userGoodListService, RestrictionService restrictionService)
        {
            _navigation = navigation;
            _userService = userService;
            _optimizeService = optimizeService;
            _userGoodListService = userGoodListService;
            _restrictionService = restrictionService;

            if (_userService.CurrentUser.UserName.Equals("Guest"))
            {
                GoodList = _userGoodListService.GuestGetAllGoods();
            }
            else
            {
                GoodList = _userGoodListService.UserGetAllGoods(_userService.CurrentUser);
            }

            PeriodList = new List<string>()
            {
                "День",
                "Тиждень",
                "Місяць",
                "Рік"
            };
            ClearFields();
        }
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати оптимізацію продуктвого кошика?",
                "Скасування оптимізації", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _navigation.GoBack();
            }
        });
        public ICommand OptimizeCommand => new DelegateCommand(async () =>
        {
            CreateOptimizationModel();
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            using (FileStream fs = new FileStream("products.json", FileMode.Create))
            {
                foreach(var good in _optimizeService.OptimizeModel.ObjectiveGoods)
                {
                    await JsonSerializer.SerializeAsync<OptimizeService.GoodToOptimize>(fs, good,options);
                }
                
                MessageBox.Show("Data has been saved to file");
            }

            using (FileStream fs = new FileStream("results.json", FileMode.Open))
            {
                OptimizeService.Result Result = await JsonSerializer.DeserializeAsync<OptimizeService.Result>(fs,options);
                _optimizeService.OptimizeModel.Result.Add(Result);
            }

            if (_userService.CurrentUser.UserName.Equals("Guest"))
            {
                _navigation.Navigate(new GuestGoodsBasketPage());
            }
            else
            {
                _navigation.Navigate(new UserGoodsBasketPage());
            }
            
        }, ()=>!string.IsNullOrEmpty(MaximumGoodPrice) && SelectedGoods.Any());

        private void CreateOptimizationModel()
        {
            
            _optimizeService.OptimizeModel.Period = Period;
            _optimizeService.OptimizeModel.MaxSum = double.Parse(MaximumGoodPrice, CultureInfo.InvariantCulture);
            foreach(var good in SelectedGoods)
            {
                OptimizeService.GoodToOptimize goodToOptimize = new OptimizeService.GoodToOptimize();
                goodToOptimize.GoodBU = good.GoodCarbohydrates;
                goodToOptimize.GoodUnit = good.GoodUnits;
                goodToOptimize.GoodPrice = good.GoodPrice;
                goodToOptimize.GoodInShopID = good.GoodInShopID;
                goodToOptimize.GoodID = good.GoodID;
                goodToOptimize.GoodAmount = good.GoodAmount;
                goodToOptimize.GoodName = good.GoodName;
               goodToOptimize.Restrictions = _restrictionService.GetGoodRestrictions(good.GoodID);
                _optimizeService.OptimizeModel.ObjectiveGoods.Add(goodToOptimize);
            }
        }

        public ICommand CancelOptimizationCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати оптимізацію продуктвого кошика?",
                "Скасування оптимізації", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
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
            }
        });

        public ICommand GoToMainCommand => new DelegateCommand(() =>
        {
        MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати обчислення?",
            "Скасування обчислень", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (_userService.CurrentUser.IsSuperUser)
                {
                    _navigation.Navigate(new AdminMainPage());
                }
                else
                {
                    if(_userService.CurrentUser.UserName.Equals("Guest"))
                    {
                        _navigation.Navigate(new GuestMainPage());
                    }
                    else
                    {
                        _navigation.Navigate(new UserMainPage());
                    }
                }
            }
        });
        public ICommand AddToSelectedGoods => new DelegateCommand(() =>
        {
            SelectedGoods.Add(SelectedGood);
        });
        public ICommand DeleteFromSelectedGoods => new DelegateCommand(() =>
        {
            SelectedGoods.Remove(ConfirmedSelectedGood);
        });
        public void ClearFields()
        {
            if(!string.IsNullOrEmpty(MaximumGoodPrice))
            {
                MaximumGoodPrice = string.Empty;
            }
            if (SelectedGoods.Any())
            {
                SelectedGoods.Clear();
            }
        }
    }
}
