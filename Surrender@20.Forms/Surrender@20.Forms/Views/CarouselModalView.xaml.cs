using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
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
    [MvxCarouselPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class CarouselModalView : MvxCarouselPage //TODO fix, rebuild, populate
    {
        public CarouselModalView()
        {
            InitializeComponent();
        }
    }
}