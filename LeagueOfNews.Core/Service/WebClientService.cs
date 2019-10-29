using HtmlAgilityPack;
using LeagueOfNews.Core.Interface;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace LeagueOfNews.Core.Model
{
    public class WebClientService : IWebClientService
    {
        private readonly IInternetConnectionService _intrernetConnecionService;
        private readonly ISettingsService _settingsService;

        public WebClientService(IInternetConnectionService intrernetConnecionService, ISettingsService settingsService)
        {
            _intrernetConnecionService = intrernetConnecionService;
            _settingsService = settingsService;
        }

        public async Task<HtmlDocument> GetPage(string url, NewsCategory page)
        {
            if (!_intrernetConnecionService.IsInternetAvailable())
            {
                return null;
            }

            switch (_settingsService[page].Website)
            {
                case NewsWebsite.Surrender:
                    return await GetPageByWebClient(url);
                case NewsWebsite.LoL:
                    return await GetPageByRequest(url);
                case NewsWebsite.DevCorner:
                    return await GetPageByRequest(url);
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

        public async Task<byte[]> GetImageAsync(string url)
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