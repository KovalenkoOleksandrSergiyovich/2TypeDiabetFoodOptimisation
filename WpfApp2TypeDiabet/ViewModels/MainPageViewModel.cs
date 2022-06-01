using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp2TypeDiabet.Pages;
using WpfApp2TypeDiabet.Services;

namespace WpfApp2TypeDiabet.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public MainPageViewModel(NavigationService navigation)
        {
            navigation.OnPageChanged += page => CurrentPage = page;
            navigation.Navigate(new PromptToLogin());
        }
        public Page CurrentPage { get; set; }   
    }
}
