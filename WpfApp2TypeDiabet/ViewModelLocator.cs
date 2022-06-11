using WpfApp2TypeDiabet.ViewModels;

namespace WpfApp2TypeDiabet
{
    public class ViewModelLocator
    {
        public AdminMainPageViewModel _adminMainPageViewModel => DependencyInjection.Resolve<AdminMainPageViewModel>();
        public AdminUserViewPageViewModel _adminUserViewPageViewModel => DependencyInjection.Resolve<AdminUserViewPageViewModel>();
        public BasketListPageViewModel _backetListPageViewModel => DependencyInjection.Resolve<BasketListPageViewModel>();
        public CalculateDataInputPageViewModel _calculateDataInputPageViewModel => DependencyInjection.Resolve<CalculateDataInputPageViewModel>();
        public GoodUserAddPageViewModel _goodPageViewModel => DependencyInjection.Resolve<GoodUserAddPageViewModel>();
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
        public GoodAddAdminPageViewModel _goodAdminPageViewModel => DependencyInjection.Resolve<GoodAddAdminPageViewModel>();
        public CalculationPreparationsPageViewModel _calculationPreparations => DependencyInjection.Resolve<CalculationPreparationsPageViewModel>();
        public GoodEditAdminPageViewModel _goodEditAdminPageViewModel => DependencyInjection.Resolve<GoodEditAdminPageViewModel>();
        public RestrictionAddAdminPageViewModel _restrictionAddAdminPageViewModel => DependencyInjection.Resolve<RestrictionAddAdminPageViewModel>();
        public RestrictionEditAdminPageViewModel _restrictionEditAdminPageViewModel => DependencyInjection.Resolve<RestrictionEditAdminPageViewModel>();
        public GoodViewAdminPageViewModel _goodViewAdminPageViewModel => DependencyInjection.Resolve<GoodViewAdminPageViewModel>();
        public RestrictionViewAdminPageViewModel _restrictionViewAdminPageViewModel => DependencyInjection.Resolve<RestrictionViewAdminPageViewModel>();
        public GoodGuestViewPageViewModel _goodGuestViewPageViewModel => DependencyInjection.Resolve<GoodGuestViewPageViewModel>();
        public RestrictionGuestViewPageViewModel _restrictionGuestViewPageViewModel => DependencyInjection.Resolve<RestrictionGuestViewPageViewModel>();
        public GoodStandartUserViewPageViewModel _goodStandartUserViewPageViewModel => DependencyInjection.Resolve<GoodStandartUserViewPageViewModel>();
        public GoodCustomUserViewPageViewModel _goodCustomUserViewPageViewModel => DependencyInjection.Resolve<GoodCustomUserViewPageViewModel>();
        public RestrictionStandartUserViewPageViewModel _restrictionStandartUserViewPageViewModel => DependencyInjection.Resolve<RestrictionStandartUserViewPageViewModel>();
        public RestrictionCustomUserViewPageViewModel _restrictionCustomUserViewPageViewModel => DependencyInjection.Resolve<RestrictionCustomUserViewPageViewModel>();
        public GoodUserAddPageViewModel _goodUserAddPageViewModel => DependencyInjection.Resolve<GoodUserAddPageViewModel>();
        public RestrictionUserAddPageViewModel _restrictionUserAddPageViewModel => DependencyInjection.Resolve<RestrictionUserAddPageViewModel>();
        public GoodUserEditPageViewModel _goodUserEditPageViewModel => DependencyInjection.Resolve<GoodUserEditPageViewModel>();
        public RestrictionUserEditPageViewModel _restrictionUserEditPageViewModel => DependencyInjection.Resolve<RestrictionUserEditPageViewModel>();
    }
}
