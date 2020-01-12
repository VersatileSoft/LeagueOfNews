using System.Collections.Generic;

namespace LeagueOfNews.Model
{
    public class Website
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Website> Subpages { get; set; }
        public string Url { get; set; }
        public string ParserName { get; set; }
    }
}