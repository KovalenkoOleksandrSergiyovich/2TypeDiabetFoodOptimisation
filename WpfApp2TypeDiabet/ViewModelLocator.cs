using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.ViewModels;

namespace WpfApp2TypeDiabet
{
    public class ViewModelLocator
    {
        public MainPageViewModel _mainViewModel => DependencyInjection.Resolve<MainPageViewModel>();
        public PromptToLoginPageViewModel _promptToLoginViewModel => DependencyInjection.Resolve<PromptToLoginPageViewModel>();
        public LoginPageViewModel _loginPageViewModel => DependencyInjection.Resolve<LoginPageViewModel>();
        public GuestMainPageViewModel _guestMainPageViewModel => DependencyInjection.Resolve<GuestMainPageViewModel>();
        public RegistrationPageViewModel _registrationPageViewModel => DependencyInjection.Resolve<RegistrationPageViewModel>();
        public UserMainPageViewModel _userMainPageViewModel => DependencyInjection.Resolve<UserMainPageViewModel>();
        public GoodsViewPageViewModel _goodsViewPageViewModel => DependencyInjection.Resolve<GoodsViewPageViewModel>();
        public GoodPageViewModel _goodPageViewModel => DependencyInjection.Resolve<GoodPageViewModel>();
        public CalculateDataInputPageViewModel _calculateDataInputPageViewModel => DependencyInjection.Resolve<CalculateDataInputPageViewModel>();
        public GoodsBasketPageViewModel _goodsBacketPageViewModel => DependencyInjection.Resolve<GoodsBasketPageViewModel>();
        public BasketListPageViewModel _backetListPageViewModel => DependencyInjection.Resolve<BasketListPageViewModel>();
        public RestrictionsPageViewModel _restrictionsPageViewModel => DependencyInjection.Resolve<RestrictionsPageViewModel>();
    }
}
