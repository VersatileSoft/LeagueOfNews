using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedListViewModel : MvxViewModel<Setting>
    {
        private string _url;
        private INewsfeedService _newsfeedService;
        private ISettingsService _settingsService;

        public ObservableCollection<Newsfeed> Newsfeeds { get; set; }
        public string Title { get; set; }
        public bool IsLoading { get; set; }

        public NewsfeedListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService)
        {
            _newsfeedService = newsfeedService;
            _settingsService = settingsService;
        }

        public override void Prepare(Setting parameter)
        {
            Title = _settingsService[parameter].Title;
            _url = _settingsService[parameter].URL;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            IsLoading = true;
            Newsfeeds = await _newsfeedService.LoadNewsfeedsAsync(_url);
            IsLoading = false;
        }
    }
}
