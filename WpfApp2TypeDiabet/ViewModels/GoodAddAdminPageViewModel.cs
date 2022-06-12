using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class GoodAddAdminPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly CategoryService _categoryService;
        private readonly GoodStateService _goodStateService;
        private readonly GoodService _goodService;
        private readonly GoodShopStateService _goodShopStateService;
        private readonly GoodInShopService _goodInShopService;
        private readonly UserService _userService;
        private readonly RestrictionService _restrictionService;

        public string GoodName { get; set; }
        public string GoodCategory { get; set; }
        public string GoodState { get; set; }
        public string GoodPrice { get; set; }
        public string GoodAmount { get; set; }
        public string GoodUnits { get; set; }
        public string GoodCarbohydrates { get; set; }
        public bool GoodIsDefault { get; set; }

        public List<string> GoodCategoryList { get; set; }
        public List<string> GoodUnitList { get; set; }
        public List<string> GoodStateList { get; set; }

        public GoodAddAdminPageViewModel(NavigationService navigation, CategoryService categoryService, GoodStateService goodStateService,
            GoodService goodService, GoodShopStateService goodShopStateService, GoodInShopService goodInShopService, UserService userService,
            RestrictionService restrictionService)
        {
            _navigation = navigation;
            _categoryService = categoryService;
            _goodStateService = goodStateService;
            _goodService = goodService;
            _goodShopStateService = goodShopStateService;
            _goodInShopService = goodInShopService;
            _userService = userService;
            _restrictionService = restrictionService;

            GoodCategoryList = _categoryService.FillCategories();
            GoodStateList = _goodStateService.FillGoodStates();

            GoodUnitList = new List<string>
            {
                "грами",
                "кілограми",
                "літри",
                "мілілітри"
            };
        }
        private void ClearFields()
        {
            if (!string.IsNullOrEmpty(GoodName))
            {
                GoodName = string.Empty;
            }
            if (!string.IsNullOrEmpty(GoodCategory))
            {
                GoodCategory = string.Empty;
            }
            if (!string.IsNullOrEmpty(GoodState))
            {
                GoodState = string.Empty;
            }
            if (!string.IsNullOrEmpty(GoodPrice))
            {
                GoodPrice = string.Empty;
            }
            if (!string.IsNullOrEmpty(GoodAmount))
            {
                GoodAmount = string.Empty;
            }
            if (!string.IsNullOrEmpty(GoodUnits))
            {
                GoodUnits = string.Empty;
            }
            if (!string.IsNullOrEmpty(GoodCarbohydrates))
            {
                GoodCarbohydrates = string.Empty;
            }
            if(GoodIsDefault)
            {
                GoodIsDefault = false;
            }
        }
        public ICommand ConfirmGoodAddAdminCommand => new DelegateCommand(() =>
        {
            if (!string.IsNullOrEmpty(GoodName) && !string.IsNullOrEmpty(GoodCategory) && !string.IsNullOrEmpty(GoodState) &&
            !string.IsNullOrEmpty(GoodPrice) && !string.IsNullOrEmpty(GoodAmount) && !string.IsNullOrEmpty(GoodUnits) &&
            !string.IsNullOrEmpty(GoodCarbohydrates))
            {
                //TODO...
                //add new good
                //_navigation.Navigate(new GoodsViewPage());
                if (CreateGood())
                {
                    if (CreateGoodInShop())
                    {
                        if (CreateGoodState())
                        {
                            if (LinkGoodToState())
                            {
                                    MessageBox.Show("Товар було успішно створено", "Додавання товару", MessageBoxButton.OK, MessageBoxImage.Information);
                                    ClearFields();
                            }
                        }
                    }
                }
            }

        }, () => !string.IsNullOrWhiteSpace(GoodName) &&
        !string.IsNullOrWhiteSpace(GoodCategory) &&
        !string.IsNullOrWhiteSpace(GoodState) &&
        !string.IsNullOrWhiteSpace(GoodPrice) &&
        !string.IsNullOrWhiteSpace(GoodAmount) &&
        !string.IsNullOrWhiteSpace(GoodUnits) &&
        !string.IsNullOrWhiteSpace(GoodCarbohydrates));
        public bool CreateGood()
        {
            string goodCreationResult;
            Goods goodToCreate = new Goods(GoodName)
            {
                CategoryId = _categoryService.GetCategory(GoodCategory).Id
            };
            //goodToCreate.CategoryId = goodToCreate.Category.Id;
            goodCreationResult = _goodService.CreateGood(goodToCreate);
            if (!goodCreationResult.Equals("Success"))
            {
                MessageBox.Show("Неможливо додати товар. Помилка " + goodCreationResult, "Додавання товару", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                //MessageBox.Show("Товар " + goodToCreate.GoodName + " було успішно стоврено", "Додавання товару", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
        }
        public bool CreateGoodInShop()
        {
            Goods good = _goodService.Good;
            string goodInShopCreationResult;
            string restrictionCreationResult;
            string restrictionName = "Стандартне обмеження";
            int maxAmount = 0;
            switch (GoodUnits)
            {
                case "мілілітри":
                    {
                        maxAmount = 2500;
                        break;
                    }
                case "літри":
                    {
                        maxAmount = 5;
                        break;
                    }
                case "грами":
                    {
                        maxAmount = 1500;
                        break;
                    }
                case "кілограми":
                    {
                        maxAmount = 2;
                        break;
                    }
            }
            restrictionCreationResult = _restrictionService.CreateRestriction(_userService.CurrentUser, restrictionName,
                "lse", maxAmount, GoodUnits);
            if (!restrictionCreationResult.Equals("Success"))
            {
                MessageBox.Show("Неможливо створити стандартне обмеження для товару. Помилка " + restrictionCreationResult, "Додавання товару", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                goodInShopCreationResult = _goodInShopService.CreateGoodInShop(_userService.CurrentUser, good.id, double.Parse(GoodPrice, CultureInfo.InvariantCulture),
                    double.Parse(GoodAmount, CultureInfo.InvariantCulture), GoodUnits, _restrictionService.Restriction.id);
                if (!goodInShopCreationResult.Equals("Success"))
                {
                    MessageBox.Show("Неможливо додати товар до магазину. Помилка " + goodInShopCreationResult, "Додавання товару", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    //MessageBox.Show("Товар було успішно додано до магазину", "Додавання товару", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
            }
        }
        public bool CreateGoodState()
        {
            string goodStateCreationResult = _goodStateService.CreateGoodState(GoodState, double.Parse(GoodCarbohydrates, CultureInfo.InvariantCulture));
            if (!goodStateCreationResult.Equals("Success"))
            {
                MessageBox.Show("Неможливо створити стан продукту. Помилка " + goodStateCreationResult, "Додавання товару", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                //MessageBox.Show("Стан продукту було успішно додано до магазину", "Додавання товару", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
        }
        public bool LinkGoodToState()
        {
            string goodShopStateCreationResult = _goodShopStateService.CreateGoodShopState(_goodInShopService.GoodInShop.id, _goodStateService.GoodState.id);
            if (!goodShopStateCreationResult.Equals("Success"))
            {
                MessageBox.Show("Неможливо поєднати продукт в магазині та його стан. Помилка " + goodShopStateCreationResult, "Додавання товару", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                //MessageBox.Show("Стан продукту було успішно поєднано з продуктом", "Додавання товару", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
        }
        public ICommand CancelGoodAddAdminCommand => new DelegateCommand(() =>
        {
            if (!string.IsNullOrEmpty(GoodName) || !string.IsNullOrEmpty(GoodCategory) || !string.IsNullOrEmpty(GoodState) ||
            !string.IsNullOrEmpty(GoodPrice) || !string.IsNullOrEmpty(GoodAmount) || !string.IsNullOrEmpty(GoodUnits) ||
            !string.IsNullOrEmpty(GoodCarbohydrates))
            {
                MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати внесення змін?",
                "Скасування внесення змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ClearFields();
                    _navigation.GoBack();
                }
            }
            else
            {
                _navigation.GoBack();
            }
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
            if (!string.IsNullOrEmpty(GoodName) || !string.IsNullOrEmpty(GoodCategory) || !string.IsNullOrEmpty(GoodState) ||
            !string.IsNullOrEmpty(GoodPrice) || !string.IsNullOrEmpty(GoodAmount) || !string.IsNullOrEmpty(GoodUnits) ||
            !string.IsNullOrEmpty(GoodCarbohydrates))
            {
                MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати внесення змін?",
                "Скасування внесення змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ClearFields();
                    _navigation.Navigate(new AdminMainPage());
                }
            }
            else
            {
                _navigation.Navigate(new AdminMainPage());
            }
        });
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати внесення змін?",
                "Скасування внесення змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _navigation.GoBack();
            }
        });
    }
}
