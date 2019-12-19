using System;
using LeagueOfNews.Forms.Interfaces;

namespace LeagueOfNews.Forms.Services
{
    public class TabsInitService : ITabsInitService
    {
        public EventHandler TabsLoaded { get; set; }
    }
}