using Autofac;
using Surrender_20.Core.ViewModel;
using System;

namespace Surrender_20.Core
{
    public class ViewModelLocator
    {
        private readonly IContainer _container;

        public ViewModelLocator()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<MainPageViewModel>();
            containerBuilder.RegisterType<NewsfeedListViewModel>();
            containerBuilder.RegisterType<NewsfeedItemViewModel>();
            containerBuilder.RegisterType<SettingsViewModel>();

            _container = containerBuilder.Build();
        }

        public MainPageViewModel MainPageViewModel => _container.Resolve<MainPageViewModel>();
        public NewsfeedListViewModel NewsfeedListViewModel => _container.Resolve<NewsfeedListViewModel>();
        public NewsfeedItemViewModel NewsfeedPageViewModel => _container.Resolve<NewsfeedItemViewModel>();
        public SettingsViewModel SettingsViewModel => _container.Resolve<SettingsViewModel>();
    }
}
