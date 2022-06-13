﻿using DevExpress.Mvvm;
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
using static WpfApp2TypeDiabet.Services.RestrictionService;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class RestrictionCustomUserViewPageViewModel : BindableBase
    {
        private NavigationService _navigation;
        private RestrictionService _restrictionService;
        private readonly UserService _userService;

        public ObservableCollection<UserGoodRestriction> UserRestrictionsList { get; set; } = new ObservableCollection<RestrictionService.UserGoodRestriction>();
        public UserGoodRestriction SelectedUserRestriction { get; set; }
        public RestrictionCustomUserViewPageViewModel(NavigationService navigation, RestrictionService restrictionService, UserService userService)
        {
            _navigation = navigation;
            _restrictionService = restrictionService;
            _userService = userService;
            FillRestrictionList();
        }
        public ICommand DeleteUserRestrictionCommand => new DelegateCommand(() =>
        {
            MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете видалити це обмеження?", "Видалення обмеження", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result==MessageBoxResult.Yes)
            {
                Restriction restriction = _restrictionService.GetRestriction(SelectedUserRestriction.RestrictionID);
                string restrictionDeletionResult = _restrictionService.DeleteRestriction(restriction);
                if (!restrictionDeletionResult.Equals("Success"))
                {
                    MessageBox.Show("Неможливо видалити обмеження. Помилка " + restrictionDeletionResult, "Видалення обмеження", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Обмеження було успішно видалено", "Видалення обмеження", MessageBoxButton.OK, MessageBoxImage.Information);
                    FillRestrictionList();
                }
            }
        });
        public ICommand GoBackCommand => new DelegateCommand(()=>
        {
            ClearList();
            _navigation.GoBack();
        });
        public ICommand GoToMainPageCommand => new DelegateCommand(() =>
        {
            ClearList();
            _navigation.Navigate(new UserMainPage());
        });
        public ICommand GoToRestrictionPageCommand => new DelegateCommand(() =>
        {
            _navigation.Navigate(new RestrictionUserAddPage());
        });
        public void FillRestrictionList()
        {
            if(UserRestrictionsList.Any())
            {
                UserRestrictionsList.Clear();
            }
            UserRestrictionsList = _restrictionService.GetUserGoodRestrictions(_userService.CurrentUser);
        }
        public void ClearList()
        {
            UserRestrictionsList.Clear();
        }
    }
}
