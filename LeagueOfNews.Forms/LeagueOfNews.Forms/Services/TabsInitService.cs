using LeagueOfNews.Forms.Interfaces;
using System;

namespace LeagueOfNews.Forms.Services
{
    public class TabsInitService : ITabsInitService
    {
        public EventHandler TabsLoaded { get; set; }
    }
}