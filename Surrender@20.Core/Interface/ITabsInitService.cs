using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.Interface
{
    public interface ITabsInitService
    {
        EventHandler TabsLoaded { get; set; }
    }
}
