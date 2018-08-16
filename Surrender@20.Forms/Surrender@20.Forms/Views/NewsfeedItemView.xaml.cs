using MvvmCross.Forms.Views;
using Surrender_20.Core.ViewModels;
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
	public partial class NewsfeedItemView : MvxContentPage<NewsfeedItemViewModel>
    {
		public NewsfeedItemView ()
		{
			InitializeComponent ();
		}
	}
}