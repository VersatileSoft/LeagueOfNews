using System;

namespace LeagueOfNews.Forms.Interfaces
{
    public interface ITabsInitService
    {
        EventHandler TabsLoaded { get; set; }
    }
}