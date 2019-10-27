using LeagueOfNews.Core.Interface;
using LeagueOfNews.Forms.Interfaces;
using System;

namespace LeagueOfNews.Forms.Services
{
    public class MasterDetailService : IMasterDetailService
    {
        public event EventHandler<MasterPageSelectArgs> OnMasterPageSelect;

        public void MasterPageSelect(NewsWebsite Page)
        {
            OnMasterPageSelect?.Invoke(this, new MasterPageSelectArgs
            {
                Page = Page
            });
        }
    }
}