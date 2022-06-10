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
        public AdminMainPageViewModel _adminMainPageViewModel => DependencyInjection.Resolve<AdminMainPageViewModel>();
        public AdminUserViewPageViewModel _adminUserViewPageViewModel => DependencyInjection.Resolve<AdminUserViewPageViewModel>();
        public BasketListPageViewModel _backetListPageViewModel => DependencyInjection.Resolve<BasketListPageViewModel>();
        public CalculateDataInputPageViewModel _calculateDataInputPageViewModel => DependencyInjection.Resolve<CalculateDataInputPageViewModel>();
        public GoodPageViewModel _goodPageViewModel => DependencyInjection.Resolve<GoodPageViewModel>();
        public GoodsBasketPageViewModel _goodsBacketPageViewModel => DependencyInjection.Resolve<GoodsBasketPageViewModel>();
        public GoodsViewPageViewModel _goodsViewPageViewModel => DependencyInjection.Resolve<GoodsViewPageViewModel>();
        public GuestMainPageViewModel _guestMainPageViewModel => DependencyInjection.Resolve<GuestMainPageViewModel>();
        public LoginPageViewModel _loginPageViewModel => DependencyInjection.Resolve<LoginPageViewModel>();
        public MainPageViewModel _mainViewModel => DependencyInjection.Resolve<MainPageViewModel>();
        public PromptToLoginPageViewModel _promptToLoginViewModel => DependencyInjection.Resolve<PromptToLoginPageViewModel>();       
        public RegistrationPageViewModel _registrationPageViewModel => DependencyInjection.Resolve<RegistrationPageViewModel>();
        public RestrictionsPageViewModel _restrictionsPageViewModel => DependencyInjection.Resolve<RestrictionsPageViewModel>();
        public UserMainPageViewModel _userMainPageViewModel => DependencyInjection.Resolve<UserMainPageViewModel>();
        public UserProfileViewModel _userProfileViewModel => DependencyInjection.Resolve<UserProfileViewModel>();
        public UserEditUserPageViewModel _userEditUserPageViewModel => DependencyInjection.Resolve<UserEditUserPageViewModel>();
        public GoodAdminPageViewModel _goodAdminPageViewModel => DependencyInjection.Resolve<GoodAdminPageViewModel>();
        public CalculationPreparationsPageViewModel _calculationPreparations => DependencyInjection.Resolve<CalculationPreparationsPageViewModel>();
    }
}
