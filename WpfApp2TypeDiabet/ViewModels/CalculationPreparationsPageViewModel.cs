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
using System.Xml;
using WpfApp2TypeDiabet.Models;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;
using static WpfApp2TypeDiabet.Services.OptimizeService;
using static WpfApp2TypeDiabet.Services.UserGoodListService;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class CalculationPreparationsPageViewModel : BindableBase
    {
        protected class JsonStructure
        {
            public double MinBU { get; set; }
            public double MaxBU { get; set; }
            public double MaxSum { get; set; }
            public int Days { get; set; }
            public ObservableCollection<OptimizeService.GoodToOptimize> Goods { get; set; } = new ObservableCollection<OptimizeService.GoodToOptimize>();
        }

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
            JsonStructure structure = new JsonStructure();
            structure.MaxSum = _optimizeService.OptimizeModel.MaxSum;
            structure.MinBU = _optimizeService.OptimizeModel.MinBU;
            structure.MaxBU = _optimizeService.OptimizeModel.MaxBU;
            structure.Days = GetNumberOfDays();
            structure.Goods = _optimizeService.OptimizeModel.ObjectiveGoods;
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            using (FileStream fs = new FileStream("products.json", FileMode.Create))
            {
                foreach (var good in _optimizeService.OptimizeModel.ObjectiveGoods)
                {
                    good.GoodPrice /= good.GoodAmount;
                }
                await JsonSerializer.SerializeAsync<JsonStructure>(fs, structure, options);
                //foreach (var good in _optimizeService.OptimizeModel.ObjectiveGoods)
                //{
                //    await JsonSerializer.SerializeAsync<OptimizeService.GoodToOptimize>(fs, good, options);
                //}

                MessageBox.Show("Data has been saved to file");
            }

            if(_optimizeService.OptimizeModel.Result.Any())
            {
                _optimizeService.OptimizeModel.Result.Clear();
            }
            using (FileStream fs = new FileStream("results.json", FileMode.Open))
            {
                OptimizeService.Result Result = await JsonSerializer.DeserializeAsync<OptimizeService.Result>(fs, options);
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

        private int GetNumberOfDays()
        {
            int days = 0;
            switch (Period)
            {
                case "День":
                    {
                        days = 1;
                        break;
                    }
                case "Тиждень":
                    {
                        days = 7;
                        break;
                    }
                case "Місяць":
                    {
                        days = 30;
                        break;
                    }
                case "Рік":
                    {
                        days = 365;
                        break;
                    }
            }
            return days;
        }

        private void CreateOptimizationModel()
        {
            _optimizeService.OptimizeModel.Period = Period;
            _optimizeService.OptimizeModel.MaxSum = double.Parse(MaximumGoodPrice, CultureInfo.InvariantCulture);
            if (_optimizeService.OptimizeModel.ObjectiveGoods.Any())
            {
                _optimizeService.OptimizeModel.ObjectiveGoods.Clear();
            }
            foreach (var good in SelectedGoods)
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
                _optimizeService.CalculateBUBorders();
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
