using System.Threading.Tasks;
using MvvmHelpers;

namespace LeagueOfNewsNew.XF.PageModels
{
    public abstract class PageModelBase : ObservableObject
    {
        /// <summary>
        /// Task called when page is appearing
        /// </summary>
        /// <returns></returns>
        public virtual Task OnLoad() => Task.CompletedTask;
    }
}
