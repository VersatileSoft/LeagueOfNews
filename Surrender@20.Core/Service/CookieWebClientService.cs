using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Surrender_20.Core.Model
{
    public class CookieWebClientService : ICookieWebClientService
    {

        public async Task<HtmlDocument> GetPage(string url)
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

        public async Task<byte[]> GetImage(string url)
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
    }
}
