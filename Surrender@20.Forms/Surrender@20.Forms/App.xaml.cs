using System;
using Xamarin.Forms;
using Surrender_20.Forms.Views;
using Xamarin.Forms.Xaml;
using MvvmCross.Forms.Core;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Surrender_20.Forms
{
	public partial class App : MvxFormsApplication
	{
		
		public App ()
		{
			InitializeComponent();
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
