using Android.Content;
using Android.Gms.Ads;
using Android.Widget;
using LeagueOfNews.Forms.Views.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdControlView), typeof(LeagueOfNews.Forms.Droid.Renderers.AdViewRenderer))]
namespace LeagueOfNews.Forms.Droid.Renderers
{
    public class AdViewRenderer : ViewRenderer<AdControlView, AdView>
    {
        private AdView adView;
        private readonly Context _context;

        public AdViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        private AdView CreateNativeAdControl()
        {
            if (adView != null)
            {
                return adView;
            }

            adView = new AdView(_context)
            {
                AdSize = AdSize.SmartBanner,
                AdUnitId = Resources.GetString(Resource.String.banner_ad_unit_id)
            };

            LinearLayout.LayoutParams adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            adView.LayoutParameters = adParams;

            adView.LoadAd(new AdRequest
                            .Builder()
#if DEBUG
                            .AddTestDevice("CAFB33A5D42F4D16D9004932F5314862") // TODO Kacpur dodaj tu swój tel do testów
#endif
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