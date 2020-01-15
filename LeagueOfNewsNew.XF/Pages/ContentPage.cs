using System;
using System.Linq;
using System.Reflection;
using Autofac;
using LeagueOfNewsNew.XF.PageModels;
using Xamarin.Forms;

namespace LeagueOfNewsNew.XF.Pages
{
    public class ContentPage<T> : ContentPage where T : PageModelBase
    {
        protected T PageModel;

        protected ContentPage()
        {
            string name = GetType().Name + "Model";
            Type type = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Name == name).FirstOrDefault();
            PageModel = (T)IoC.Container.Resolve(type);

            //TODO Appearing ins not working but should be used here
            Appearing += delegate { PageModel.OnLoad(); };
            // page.BindingContextChanged += delegate { pageModel.OnLoad(); };
            BindingContext = PageModel;
        }
    }
}
