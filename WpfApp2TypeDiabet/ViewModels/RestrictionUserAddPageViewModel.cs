using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2TypeDiabet.Models;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;
using static WpfApp2TypeDiabet.Services.RestrictionService;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class RestrictionUserAddPageViewModel : BindableBase
    {
        private string _goodNameForSearch;

        private readonly NavigationService _navigation;
        private readonly GoodService _goodService;
        private readonly RestrictionService _restrictionService;
        private readonly GoodInShopService _goodInShopService;
        private readonly UserService _userService;
        private readonly UserGoodListService _userGoodListService;
        private readonly UserRestrictionListService _userRestrictionListService;

        public string GoodNameForSearch 
        {
            get => _goodNameForSearch; 
            set
            {
                _goodNameForSearch = value;
                FillRestrictionList(_userService.CurrentUser, value);
            }
        }

        public Goods SelectedGood { get; set; }
        public ObservableCollection<string> GoodList { get; set; }
        public List<string> UnitTypeList { get; set; }
        public List<string> RestrictionTypeList { get; set; }
        public string RestrictionName { get; set; }
        public string RestrictionType { get; set; }
        public string UnitType { get; set; }
        public string RestrictionAmount { get; set; }
        public Restriction SelectedGoodRestriction { get; set; }
        public ObservableCollection<UserRestriction> SelectedGoodRestrictions { get; set; } = new ObservableCollection<UserRestriction>();
        public RestrictionUserAddPageViewModel(NavigationService navigation, GoodService goodService, RestrictionService restrictionService, 
            GoodInShopService goodInShopService, UserService userService, UserGoodListService userGoodListService, UserRestrictionListService userRestrictionListService)
        {
            _navigation = navigation;
            _goodService = goodService;
            _restrictionService = restrictionService;
            _goodInShopService = goodInShopService;
            _userService = userService;
            _userGoodListService = userGoodListService;
            _userRestrictionListService = userRestrictionListService;

            UnitTypeList = new List<string>
            {
                "кг",
                "г",
                "л",
                "мл"
            };
            RestrictionTypeList = new List<string>
            {
                "Менше ніж",
                "Більше ніж",
                "Менше або дорівнює",
                "Більше або дорівнює",
                "Дорівнює"
            };
            FillGoodsList(_userService.CurrentUser);
        }
        private void FillRestrictionList(User currentUser, string value)
        {
            if(!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
            {
                SelectedGood = _goodService.GetGood(value);
                if(SelectedGoodRestrictions.Any())
                {
                    SelectedGoodRestrictions.Clear();
                }
                if(SelectedGood != null)
                {
                    SelectedGoodRestrictions = _restrictionService.GetUserRestrictions(currentUser, SelectedGood);
                }
            }
        }
        public void FillGoodsList(User user)
        {
            GoodList = _userGoodListService.GetUserGoodList(user);
        }
        public ICommand GoBackCommand => new DelegateCommand(()=>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати зміни?","Скасування змін",MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                ClearFields();
                _navigation.GoBack();
            }
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати зміни?", "Скасування змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearFields();
                _navigation.Navigate(new UserMainPage());
            }
        });

        private void ClearFields()
        {
            if(!string.IsNullOrEmpty(GoodNameForSearch))
            {
                GoodNameForSearch = string.Empty;
            }
            if(!string.IsNullOrEmpty(RestrictionName))
            {
                RestrictionName = string.Empty;
            }
            if (!string.IsNullOrEmpty(RestrictionAmount))
            {
                RestrictionAmount = string.Empty;
            }
        }

        public ICommand AddRestrictionCommand => new DelegateCommand(() =>
        {
            _goodInShopService.GetGoodInShop(SelectedGood);
            GoodInShop goodInShop = _goodInShopService.Goods.First();
            string comparator = _restrictionService.ComparatorFromString(RestrictionType);
            string restrictionCreationResult = _restrictionService.CreateRestriction(_userService.CurrentUser, RestrictionName, comparator, 
                double.Parse(RestrictionAmount, CultureInfo.InvariantCulture), UnitType);
            if(!restrictionCreationResult.Equals("Success"))
            {
                MessageBox.Show("Неможливо створити обмеження. Помилка "+restrictionCreationResult ,"Додавання обмеження",MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                //MessageBox.Show("Обмеження було успішно створено", "Додавання обмеження", MessageBoxButton.OK, MessageBoxImage.Information);
                string goodInShopCreationResult = _goodInShopService.CreateGoodInShop(_userService.CurrentUser,goodInShop.GoodId, goodInShop.GoodPrice, 
                    goodInShop.GoodAmount,goodInShop.GoodUnits, _restrictionService.Restriction.id);
                if (!goodInShopCreationResult.Equals("Success"))
                {
                    MessageBox.Show("Неможливо додати обмеження до бази даних. Помилка " + goodInShopCreationResult, "Додавання обмеження", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string userRestrictionCreationResult = _userRestrictionListService.CreateUserRestrictionList(_userService.CurrentUser.id, _restrictionService.Restriction.id);

                    if (!userRestrictionCreationResult.Equals("Success"))
                    {
                        MessageBox.Show("Неможливо пожэднати обмеження з користувачем. Помилка " + userRestrictionCreationResult, "Додавання обмеження", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    { 
                        MessageBox.Show("Обмеження було успішно додано до бази даних", "Додавання обмеження", MessageBoxButton.OK, MessageBoxImage.Information);
                        ClearFields();
                    }
                }
            }
        }, ()=>!string.IsNullOrEmpty(RestrictionName) && 
        !string.IsNullOrEmpty(RestrictionAmount) &&
        !string.IsNullOrEmpty(GoodNameForSearch));
        public ICommand CancelRestrictionAddCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати зміни?", "Скасування змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearFields();
                _navigation.Navigate(new UserMainPage());
            }
        });
    }
}
