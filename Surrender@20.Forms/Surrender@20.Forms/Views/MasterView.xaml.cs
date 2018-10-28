using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Forms.ViewModels;
using Xamarin.Forms.Xaml;

namespace Surrender_20.Forms.Views
{
    [XamlCompilation (XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
    public partial class MasterView : MvxContentPage<MasterViewModel>
	{
		public MasterView ()
		{
			InitializeComponent ();
		}
    }
}