using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Surrender_20.Forms.Views.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdControlView), typeof(Surrender_20.Forms.Droid.Renderers.AdViewRenderer))]
namespace Surrender_20.Forms.Droid.Renderers
{
    public class AdViewRenderer : ViewRenderer<AdControlView, AdView>
    {

        AdView adView;
        Context _context;

        public AdViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        AdView CreateNativeAdControl()
        {
            if (adView != null)
                return adView;
            
            adView = new AdView(_context);
            adView.AdSize = AdSize.SmartBanner;
            adView.AdUnitId = Resources.GetString(Resource.String.banner_ad_unit_id);

            var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            adView.LayoutParameters = adParams;

            adView.LoadAd(new AdRequest
                            .Builder()
                            .Build());
            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AdControlView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                SetNativeControl(CreateNativeAdControl());
            }
        }
    }
}
