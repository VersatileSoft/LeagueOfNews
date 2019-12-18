using System;
using System.Collections.Generic;
using System.Text;

namespace LeagueOfNews.Model
{
    public class Website
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Website> Subpages { get; set; }
        public string Url { get; set; }
    }
}
