using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20.Core.Service
{
    public class NewsfeedPageService : INewsfeedPageService
    {
        public async Task<string> ParseNewsfeed(Uri Url)
        {         
            var doc = await new HtmlWeb().LoadFromWebAsync(Url.AbsoluteUri);
            var nodes = doc.DocumentNode.InnerHtml;
            return nodes;
        }
    }
}
