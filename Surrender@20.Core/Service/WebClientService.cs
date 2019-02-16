using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Surrender_20.Core.Model
{
    public class WebClientService : IWebClientService
    {

        private readonly IInternetConnectionService _intrernetConnecionService;

        public WebClientService(IInternetConnectionService intrernetConnecionService)
        {
            _intrernetConnecionService = intrernetConnecionService;
        }

        public async Task<HtmlDocument> GetPage(string url, NewsCategory page)
        {
            if (!_intrernetConnecionService.IsInternetAvailable())
            {
                return null;
            }

            switch (page)
            {
                case NewsCategory.SurrenderHome:
                case NewsCategory.ESports:
                case NewsCategory.PBE:
                case NewsCategory.RedPosts:
                case NewsCategory.Rotations:
                case NewsCategory.Releases: return await GetPageByWebClient(url);
                case NewsCategory.Official: return await GetPageByRequest(url);
            }
            return null;
        }

        private async Task<HtmlDocument> GetPageByRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";

            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            Stream stream = response.GetResponseStream();

            using (StreamReader reader = new StreamReader(stream))
            {
                string html = reader.ReadToEnd();
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                return doc;
            }
        }

        private async Task<HtmlDocument> GetPageByWebClient(string url)
        {
            return await new HtmlWeb().LoadFromWebAsync(url);
        }

        public async Task<byte[]> GetImage(string url)
        {

            if (!_intrernetConnecionService.IsInternetAvailable())
            {
                return null;
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";
                // request.AllowWriteStreamBuffering = true;
                request.Timeout = 30000;

                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                Stream stream = response.GetResponseStream();

                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            catch { return null; }
        }
    }
}