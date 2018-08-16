using System;
using Xamarin.Forms;
using Surrender_20.Forms.Views;
using Xamarin.Forms.Xaml;
using MvvmCross.Forms.Core;
using MvvmCross;
using Surrender_20.Core.Interface;
using Surrender_20.Forms.Services;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Surrender_20.Forms
{
	public partial class App : MvxFormsApplication
	{
		
		public App ()
		{
			InitializeComponent();
            Mvx.LazyConstructAndRegisterSingleton<IOperatingSystemService, OperatingSystemService>();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
