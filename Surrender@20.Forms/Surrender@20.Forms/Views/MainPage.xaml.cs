using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Core.ViewModels;
using Xamarin.Forms.Xaml;

namespace Surrender_20.Forms.Views
{
    [MvxTabbedPagePresentation(TabbedPosition.Root)]
    public partial class MainPage : MvxTabbedPage<MainPageViewModel>
    {
        public MainPage()
        {
            InitializeComponent();

            
        }
    }
}