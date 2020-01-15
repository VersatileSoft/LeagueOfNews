using Autofac;
using LeagueOfNewsNew.XF.PageModels;
using LeagueOfNewsNew.XF.Services;

namespace LeagueOfNewsNew.XF
{
    public class IoC
    {

        public static IContainer Container;
        private static readonly ContainerBuilder builder = new ContainerBuilder();

        public static void Initialize()
        {
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<PageModelModule>();
            Container = builder.Build();
        }
    }
}
