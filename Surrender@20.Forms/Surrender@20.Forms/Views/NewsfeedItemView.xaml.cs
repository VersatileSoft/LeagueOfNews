using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Core.ViewModels;
using Surrender_20.Forms.ViewModels;
using Xamarin.Forms.Xaml;

namespace Surrender_20.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class NewsfeedItemView : MvxContentPage<Core.ViewModels.NewsfeedItemViewModel>
    {
		public NewsfeedItemView ()
		{
			InitializeComponent ();
		}
	}
}