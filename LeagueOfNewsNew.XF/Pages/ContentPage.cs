using System;
using System.Linq;
using System.Reflection;
using Autofac;
using LeagueOfNewsNew.XF.PageModels;
using Xamarin.Forms;

namespace LeagueOfNewsNew.XF.Pages
{
    public abstract class ContentPage<TPageModel> : ContentPage where TPageModel : PageModelBase
    {
        protected TPageModel PageModel;

        protected ContentPage() => InitPage();

        protected void InitPage()
        {
            string name = GetType().Name + "Model";
            Type type = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Name == name).FirstOrDefault();
            PageModel = (TPageModel)IoC.Container.Resolve(type);
            Appearing += delegate { PageModel.OnLoad(); };
            BindingContext = PageModel;
        }
    }

    public abstract class ContentPage<TPageModel, TParam> : ContentPage<TPageModel> where TPageModel : PageModelBase<TParam>
    {
        public ContentPage(TParam param)
        {
            InitPage();
            PageModel.Param = param;
        }
    }
}
