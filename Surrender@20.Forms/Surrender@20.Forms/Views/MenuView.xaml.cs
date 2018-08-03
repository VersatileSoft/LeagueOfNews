using MvvmCross.Forms.Presenters.Attributes;
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
    [MvxTabbedPagePresentation(TabbedPosition.Tab)]
    public partial class MenuView : MvxContentPage<MenuViewModel>
    {
		public MenuView ()
		{
			InitializeComponent ();

            if (Parent is MasterDetailPage md)
            {
                md.MasterBehavior = MasterBehavior.Popover;
                md.IsPresented = true;
            }
        }
	}
}