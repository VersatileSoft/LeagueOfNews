using FFImageLoading;
using FFImageLoading.Config;
using MvvmCross.Forms.Core;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LeagueOfNews.Forms
{
    public partial class App : MvxFormsApplication
    {
        public App()
        {
            InitializeComponent();

            Configuration config = new Configuration()
            {
                ClearMemoryCacheOnOutOfMemory = true,
                FadeAnimationForCachedImages = true,
                AllowUpscale = true,
                AnimateGifs = false,
                DownsampleInterpolationMode = FFImageLoading.Work.InterpolationMode.High,
                ExecuteCallbacksOnUIThread = true
            };
            ImageService.Instance.Initialize(config);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}