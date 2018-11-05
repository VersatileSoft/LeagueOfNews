using Surrender_20.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.Service
{
    public class TabsInitService : ITabsInitService
    {
        public EventHandler TabsLoaded { get; set; }
    }
}
