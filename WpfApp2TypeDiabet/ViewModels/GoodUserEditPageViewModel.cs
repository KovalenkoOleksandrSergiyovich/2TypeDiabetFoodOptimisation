using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class GoodUserEditPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly CategoryService _categoryService;

        public string GoodName { get; set; }
        public string GoodCategory { get; set; }
        public string GoodState { get; set; }
        public string GoodPrice { get; set; }
        public string GoodAmount { get; set; }
        public string GoodUnits { get; set; }
        public string GoodCarbohydrates { get; set; }
        public List<string> GoodCategoryList { get; set; }

        public GoodUserEditPageViewModel(NavigationService navigation, CategoryService categoryService)
        {
            _navigation = navigation;
            _categoryService = categoryService;
            GoodCategoryList = _categoryService.FillCategories();
        }
        public ICommand GoBackCommand => new DelegateCommand(()=>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати внесення змін?", "Скасування змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ClearFields();
                _navigation.GoBack();
            }
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(()=>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати внесення змін?", "Скасування змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                ClearFields();
                _navigation.Navigate(new UserMainPage());
            }
        });
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
        }
        public ICommand ConfirmGoodEditCommand => new DelegateCommand(() =>
        {
            if (!string.IsNullOrEmpty(GoodName) && !string.IsNullOrEmpty(GoodCategory) && !string.IsNullOrEmpty(GoodState) &&
            !string.IsNullOrEmpty(GoodPrice) && !string.IsNullOrEmpty(GoodAmount) && !string.IsNullOrEmpty(GoodUnits) &&
            !string.IsNullOrEmpty(GoodCarbohydrates))
            {
                //TODO...
                //add new good
                _navigation.Navigate(new GoodCustomUserViewPage());
                ClearFields();
            }

        }, () => !string.IsNullOrWhiteSpace(GoodName) &&
        !string.IsNullOrWhiteSpace(GoodCategory) &&
        !string.IsNullOrWhiteSpace(GoodState) &&
        !string.IsNullOrWhiteSpace(GoodPrice) &&
        !string.IsNullOrWhiteSpace(GoodAmount) &&
        !string.IsNullOrWhiteSpace(GoodUnits) &&
        !string.IsNullOrWhiteSpace(GoodCarbohydrates));
        public ICommand CancelGoodEditCommand => new DelegateCommand(() =>
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
                    _navigation.Navigate(new GoodCustomUserViewPage());
                }
            }
            else
            {
                _navigation.Navigate(new GoodCustomUserViewPage());
            }
        });
    }
}
