using HtmlAgilityPack;
using System.Threading.Tasks;

namespace LeagueOfNews.Core.Interface
{
    public interface IWebClientService
    {
        Task<byte[]> GetImageAsync(string url);
        Task<HtmlDocument> GetPage(string url, NewsCategory page); //Why do we need url AND page? //Because there is 2 different ways to download pages  // xD
        Task<string> GetNewsUrlFromRedirect(string originalNewsUrl);
    }
}