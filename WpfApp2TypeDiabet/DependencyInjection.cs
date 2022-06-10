﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2TypeDiabet.Services;
using WpfApp2TypeDiabet.ViewModels;

namespace WpfApp2TypeDiabet
{
    public static class DependencyInjection
    {
        private static readonly ServiceProvider _provider;

        static DependencyInjection()
        {
            var services = new ServiceCollection();



            //реєструємо view models
            services.AddSingleton<AdminMainPageViewModel>();
            services.AddTransient<AdminUserViewPageViewModel>();
            services.AddSingleton<BasketListPageViewModel>();
            services.AddTransient<CalculateDataInputPageViewModel>();
            services.AddSingleton<GoodPageViewModel>();
            services.AddSingleton<GoodsBasketPageViewModel>();
            services.AddSingleton<GoodsViewPageViewModel>();
            services.AddSingleton<GuestMainPageViewModel>();
            services.AddTransient<LoginPageViewModel>();
            services.AddSingleton<MainPageViewModel>();
            services.AddSingleton<PromptToLoginPageViewModel>();
            services.AddTransient<RegistrationPageViewModel>();
            services.AddSingleton<RestrictionsPageViewModel>();
            services.AddSingleton<UserMainPageViewModel>();
            services.AddTransient<UserProfileViewModel>();
            services.AddTransient<UserEditUserPageViewModel>();
            services.AddSingleton<GoodAdminPageViewModel>();
            services.AddSingleton<CalculationPreparationsPageViewModel>();

            //реєструємо сервіси
            services.AddSingleton<NavigationService>();
            services.AddSingleton<UserService>();

            _provider = services.BuildServiceProvider();

        }


        public static T Resolve<T>() => _provider.GetRequiredService<T>();
    }
}
