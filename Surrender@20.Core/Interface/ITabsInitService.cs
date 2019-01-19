using System;

namespace Surrender_20.Core.Interface
{
    public interface ITabsInitService
    {
        EventHandler TabsLoaded { get; set; }
    }
}