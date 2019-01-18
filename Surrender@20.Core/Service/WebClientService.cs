using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Surrender_20.Core.Model
{
    public class WebClientService : IWebClientService
    {

        public async Task<HtmlDocument> GetPage(string url, Pages page)
        {
            switch (page)
            {
                case Pages.SurrenderHome:
                case Pages.ESports:
                case Pages.PBE:
                case Pages.RedPosts:
                case Pages.Rotations:
                case Pages.Releases: return await GetPageByWebClient(url);
                case Pages.Official: return await GetPageByRequest(url);
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
