using Autofac;
using PasswordManagerApp.Services;
using PasswordManagerApp.ViewModels;
using System;

namespace PasswordManagerApp
{
    public class AppContainer
    {
        private static IContainer myContainer;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NavigationService>().As<INavigationService>();
            builder.RegisterType<RepositoryService>().As<IRepositoryService>();

            builder.RegisterType<HomePageViewModel>();
            builder.RegisterType<LoginPageViewModel>();
            builder.RegisterType<RegisterMasterPasswordPageViewModel>();

            myContainer = builder.Build();
        }

        public static T Resolve<T>()
        {
            return myContainer.Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            return myContainer.Resolve(type);
        }
    }
}
