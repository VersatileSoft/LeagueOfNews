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

            doc.DocumentNode.SelectSingleNode("//div[@class='blog-pager']").Remove();
            doc.DocumentNode.SelectSingleNode("//div[@class='mobile footer']").Remove();

            RemoveHrefAttributes(doc);

            var nodes = doc.DocumentNode.InnerHtml;
            return nodes;
        }

        private void RemoveHrefAttributes(HtmlDocument html)
        {
            var elementsWithHrefAttribute = html.DocumentNode.SelectNodes("//@href");

            if (elementsWithHrefAttribute != null)
            {
                foreach (var element in elementsWithHrefAttribute)
                {
                    element.Attributes["href"].Remove();
                }
            }
        }

    }
}
