using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Surrender_20.Core.Interface
{
    public interface IWebClientService
    {
        Task<byte[]> GetImage(string url);
        Task<HtmlDocument> GetPage(string url, NewsCategory page); //Why do we need url AND page?
    }
}