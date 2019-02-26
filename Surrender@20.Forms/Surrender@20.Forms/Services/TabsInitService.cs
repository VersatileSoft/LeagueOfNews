using Surrender_20.Forms.Interfaces;
using System;

namespace Surrender_20.Forms.Services
{
    public class TabsInitService : ITabsInitService
    {
        public EventHandler TabsLoaded { get; set; }
    }
}