﻿using Surrender_20.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Surrender_20.Core.Interface
{
    public interface INewsfeedService
    {
        Task<ObservableCollection<Newsfeed>> LoadNewsfeedsAsync(string url, bool fromSurrender);
        Task<ObservableCollection<Newsfeed>> LoadMoreNewsfeeds();
    }
}
