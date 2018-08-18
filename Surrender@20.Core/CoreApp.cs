using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Surrender_20.Core.ViewModels;

namespace Surrender_20.Core
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
        }
    }
}
