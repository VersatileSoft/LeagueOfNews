using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeagueOfNews.Forms.Interfaces
{
    public interface IChromeCustomTabService
    {
        Task StartChromCustomTab(string url);
    }
}
