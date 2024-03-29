﻿using DevExpress.Mvvm;
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
    public class GuestMainPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;

        public GuestMainPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;
        }
        public ICommand GuestGoodsListCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new GoodGuestViewPage());
        });
        public ICommand RestrictionsListCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new RestrictionGuestViewPage());
        });
        public ICommand CalculationDataInputCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new CalculateDataInputPage());
        });
        public ICommand ExitCommand => new DelegateCommand(() =>
        {
            App.Current.Windows[0].Close();
        });
    }
}
