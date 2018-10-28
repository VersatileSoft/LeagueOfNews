﻿using MvvmCross.Forms.Views;
using Surrender_20.Core.ViewModels;
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