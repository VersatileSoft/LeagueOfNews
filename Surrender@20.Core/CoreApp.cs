using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace Surrender_20.Core
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
