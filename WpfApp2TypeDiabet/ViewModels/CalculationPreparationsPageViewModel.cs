using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class CalculationPreparationsPageViewModel : BindableBase
    {
        private readonly NavigationService _navigation;

        public CalculationPreparationsPageViewModel(NavigationService navigation)
        {
            _navigation = navigation;     
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
    }
}
