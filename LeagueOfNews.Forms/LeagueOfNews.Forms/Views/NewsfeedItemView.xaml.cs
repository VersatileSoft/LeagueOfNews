using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using LeagueOfNews.Forms.ViewModels;
using Xamarin.Forms.Xaml;

namespace LeagueOfNews.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class NewsfeedItemView : MvxContentPage<NewsfeedItemViewModel>
    {
        public NewsfeedItemView()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            NewsfeedItemViewModel vm = ViewModel;
        }
    }
}