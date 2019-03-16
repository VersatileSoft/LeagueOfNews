using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace LeagueOfNews.Core
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            base.Initialize();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
        }
    }
}