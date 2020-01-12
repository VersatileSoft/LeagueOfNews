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
            page.BindingContext = IoC.container.Resolve(type);
        }
    }
}
