using LeagueOfNews.Core.Interface;
using System;

namespace LeagueOfNews.Forms.Interfaces
{
    public interface IMasterDetailService
    {
        void MasterPageSelect(NewsWebsite Page);
        event EventHandler<MasterPageSelectArgs> OnMasterPageSelect;
    }

    public class MasterPageSelectArgs
    {
        public NewsWebsite Page { get; set; }
    }
}