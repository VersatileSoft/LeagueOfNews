using Autofac;

namespace LeagueOfNewsNew.XF.PageModels
{
    public class PageModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAssemblyTypes(typeof(PageModelModule).Assembly).SingleInstance();
        }
    }
}
