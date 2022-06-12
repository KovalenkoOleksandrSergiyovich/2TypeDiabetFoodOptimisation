using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2TypeDiabet.Models;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;
using static WpfApp2TypeDiabet.Services.UserGoodListService;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class GoodViewAdminPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly GoodService _goodService;
        private readonly UserGoodListService _userGoodListService;

        public ObservableCollection<UserGood> GoodsList { get; set; } = new ObservableCollection<UserGoodListService.UserGood>();
        public UserGood SelectedGood { get; set; }

        public GoodViewAdminPageViewModel(NavigationService navigation, UserGoodListService userGoodListService,GoodService goodService)
        {
            _navigation = navigation;
            _userGoodListService = userGoodListService;
            _goodService = goodService;
            FillGoodsListView();
        }
        private void FillGoodsListView()
        {
            if (GoodsList.Any())
            {
                GoodsList.Clear();
            }
            GoodsList = _userGoodListService.GetStandartGoods();
        }
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
           _navigation.Navigate(new AdminMainPage());
        });
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            _navigation.GoBack();
        });
        public ICommand GoToGoodPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GoodAddAdminPage());
        });
        public ICommand DeleteAdminGoodCommand => new DelegateCommand(() =>
        {
            Goods goodToDelete = _goodService.GetGood(SelectedGood.GoodName);
            MessageBoxResult confirmation = MessageBox.Show("Ви впевнені, що хочете видалити товар " + goodToDelete.GoodName, "Видалення товару", MessageBoxButton.YesNo, MessageBoxImage.Question);
            /*_goodInShopService.GetGoodInShop(goodToDelete);
            GoodInShop goodInShop = _goodInShopService.GoodInShop;
            _userGoodListService.DeleteUserGoodList(_userService.CurrentUser.id, goodInShop.id);
            _goodInShopService.DeleteGoodInShop();*/
            if (confirmation == MessageBoxResult.Yes)
            {
                string result = _goodService.DeleteGood(goodToDelete);
                if (!result.Equals("Success"))
                {
                    MessageBox.Show("Неможливо видалити товар. Помилка " + result, "Видалення товару", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Товар " + goodToDelete.GoodName + " було успішно видалено", "Видалення товару", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillGoodsListView();
                }
            }
        });
    }
}
