using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.ViewModels.Android
{
    public class TabbedRootViewModel : MvxViewModel
    {
        public NewsfeedListViewModel Recycler { get; private set; }

        public TabbedRootViewModel()
        {
            Recycler = new NewsfeedListViewModel();
        }
    }
}
