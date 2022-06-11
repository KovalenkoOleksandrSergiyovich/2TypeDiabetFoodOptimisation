using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
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
    public class RestrictionUserEditPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;
        public string GoodNameForSearch { get; set; }
        public Goods SelectedGood { get; set; }
        public string RestrictionType { get; set; }
        public string UnitType { get; set; }
        public string UnitAmount { get; set; }
        public Restriction SelectedGoodRestriction { get; set; }
        public RestrictionUserEditPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;
        }
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати зміни?", "Скасування змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _navigation.GoBack();
            }
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати зміни?", "Скасування змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _navigation.Navigate(new UserMainPage());
            }
        });
        public ICommand AddRestrictionCommand => new DelegateCommand(() =>
        {

        });
        public ICommand CancelRestrictionAddCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете скасувати зміни?", "Скасування змін", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                _navigation.Navigate(new UserMainPage());
            }
        });
    }
}
