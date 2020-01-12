using System.IO;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using LeagueOfNews.Core.Interface;

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

            return _settingsService[page].Website switch
            {
                NewsWebsite.Surrender => await GetPageByWebClient(url),
                NewsWebsite.LoL => await GetPageByRequest(url),
                NewsWebsite.DevCorner => await GetPageByRequest(url),
                _ => null,
            };
        }

        private async Task<HtmlDocument> GetPageByRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Linux; Android 10;) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.79 Mobile Safari/537.36";

            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            Stream stream = response.GetResponseStream();

            using StreamReader reader = new StreamReader(stream);
            string html = reader.ReadToEnd();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            response.Close();
            return doc;
        }

        private async Task<HtmlDocument> GetPageByWebClient(string url) => await new HtmlWeb().LoadFromWebAsync(url);

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
                request.UserAgent = "Mozilla/5.0 (Linux; Android 10;) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.79 Mobile Safari/537.36";
                request.KeepAlive = false;
                request.Timeout = 10000;

                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                Stream stream = response.GetResponseStream();

                using MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);

                response.Close();
                return ms.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> GetNewsUrlFromRedirect(string originalNewsUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(originalNewsUrl);
            request.Method = "HEAD";
            request.UserAgent = "Mozilla/5.0 (Linux; Android 10;) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.79 Mobile Safari/537.36";
            request.AllowAutoRedirect = true;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 10000;

            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            string urlFromRedirect = response.ResponseUri.AbsoluteUri;
            response.Close();

            return urlFromRedirect;
        }
    }
}