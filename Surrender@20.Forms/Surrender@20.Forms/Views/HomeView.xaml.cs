using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surrender_20.Forms.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomeView : MvxContentPage
    {
		public HomeView ()
		{
			InitializeComponent ();
		}
	}
}