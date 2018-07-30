using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20.Model
{
    public class Newsfeed
    {
        //Basic model for now, probably to change
        public string Time { get; set; }

        public string Title { get; set; }

        public HtmlNode Content { get; set; }
    }
}
