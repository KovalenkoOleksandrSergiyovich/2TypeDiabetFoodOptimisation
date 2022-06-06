﻿using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class RestrictionsPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;

        public RestrictionsPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;
        }
        public ICommand GoBackCommand => new DelegateCommand(() =>
        {
            _navigation.GoBack();
        });
        public ICommand GoToMainCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new UserMainPage());
        });
    }
}
