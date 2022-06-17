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
    public class GoodCustomUserViewPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        private readonly GoodService _goodService;
        private readonly UserService _userService;
        private readonly UserGoodListService _userGoodListService;

        public ObservableCollection<GoodToOptimize> UserGoodsList { get; set; } = new ObservableCollection<UserGoodListService.GoodToOptimize>();
        public GoodToOptimize SelectedUserGood { get; set; }

        public GoodCustomUserViewPageViewModel(NavigationService navigation, UserGoodListService userGoodListService, 
            UserService userService, GoodService goodService)
        {
            _navigation = navigation;
            _goodService = goodService;
            _userService = userService;
            _userGoodListService = userGoodListService;
            FillGoodsListView();

        }

        private void FillGoodsListView()
        {
            if (UserGoodsList.Any())
            {
                UserGoodsList.Clear();
            }
            UserGoodsList = _userGoodListService.GetUserGoods(_userService.CurrentUser);
        }

        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            _navigation.GoBack();
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new UserMainPage());
        });
        public ICommand GoToGoodPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GoodUserAddPage());
        });
        public ICommand DeleteUserGoodCommand => new DelegateCommand(() =>
        {
            Goods goodToDelete = _goodService.GetGood(SelectedUserGood.GoodName);
            MessageBoxResult confirmation = MessageBox.Show("Ви впевнені, що хочете видалити товар "+ goodToDelete.GoodName, "Видалення товару",MessageBoxButton.YesNo, MessageBoxImage.Question);
            /*_goodInShopService.GetGoodInShop(goodToDelete);
            GoodInShop goodInShop = _goodInShopService.GoodInShop;
            _userGoodListService.DeleteUserGoodList(_userService.CurrentUser.id, goodInShop.id);
            _goodInShopService.DeleteGoodInShop();*/
            if(confirmation == MessageBoxResult.Yes)
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
