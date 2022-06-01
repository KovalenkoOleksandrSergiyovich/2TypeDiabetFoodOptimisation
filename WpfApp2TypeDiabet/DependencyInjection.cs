using Microsoft.Extensions.DependencyInjection;
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
            services.AddSingleton<MainPageViewModel>();
            services.AddSingleton<PromptToLoginPageViewModel>();
            services.AddSingleton<LoginPageViewModel>();
            services.AddSingleton<RegistrationPageViewModel>();
            
            //реєструємо сервіси
            services.AddSingleton<NavigationService>();

            _provider = services.BuildServiceProvider();
        }


        public static T Resolve<T>() => _provider.GetRequiredService<T>();
    }
}
