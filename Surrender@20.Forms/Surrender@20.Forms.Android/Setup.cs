using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Surrender_20.Core;
using Surrender_20.Core.Interface;
using Surrender_20.Forms.Droid.Services;
using Surrender_20.Forms.Interfaces;
using Surrender_20.Forms.Services;
using Surrender_20.Forms.ViewModels;

namespace Surrender_20.Forms.Droid
{
    public sealed class Setup : MvxFormsAndroidSetup<CoreApp, App>
    {
        protected override void InitializeLastChance()
        {
            Mvx.IoCProvider.RegisterSingleton(typeof(IOperatingSystemService), new OperatingSystemService()); //TODO move to InitializeFirstChance
            Mvx.IoCProvider.RegisterSingleton(typeof(IMasterDetailService), new MasterDetailService());       //TODO same /\
            Mvx.IoCProvider.RegisterSingleton(typeof(INotificationService), new NotificationService());       //TODO same /\
            Mvx.IoCProvider.RegisterSingleton(typeof(ISaveDataService), new SaveDataService());       //TODO same /\
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, MvxAppStart<RootPageViewModel>>();

            base.InitializeLastChance(); //TODO remove (check if work)
        }

        //public override IEnumerable<Assembly> GetViewModelAssemblies()
        //{
        //    List<Assembly> list = new List<Assembly>();
        //    list.AddRange(base.GetViewModelAssemblies());
        //    list.Add(typeof(NewsfeedItemViewModel).Assembly);
        //    return list.ToArray();
        //}
    }
}