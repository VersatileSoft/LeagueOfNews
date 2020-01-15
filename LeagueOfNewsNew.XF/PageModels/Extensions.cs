using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Xamarin.Forms;

namespace LeagueOfNewsNew.XF.PageModels
{
    public static class Extensions
    {
        public static void SetPageModel(this Page page)
        {
            string name = page.GetType().Name + "Model";
            Type type = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Name == name).FirstOrDefault();
            PageModelBase pageModel = (PageModelBase)IoC.Container.Resolve(type);

            //TODO Appearing ins not working but should be used here
            page.Appearing += delegate { pageModel.OnLoad(); };
            // page.BindingContextChanged += delegate { pageModel.OnLoad(); };
            page.BindingContext = pageModel;
        }
    }
}
