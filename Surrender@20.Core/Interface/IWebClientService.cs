using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20.Core.Interface
{
    public interface IWebClientService
    {
        Task<byte[]> GetImage(string url);
        Task<HtmlDocument> GetPage(string url, Pages page);
    }
}
