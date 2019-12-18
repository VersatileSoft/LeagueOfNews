using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeagueOfNews.Model;
using LeagueOfNews.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeagueOfNews.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsfeedController : ControllerBase
    {
        private INewsfeedService _newsfeedService;
        public NewsfeedController(INewsfeedService newsfeedService)
        {
            _newsfeedService = newsfeedService;
        }

        [HttpGet("{websiteId}")]
        public async Task<IEnumerable<Newsfeed>> Get(int websiteId, int page = 1)
        {
            return await _newsfeedService.GetNewsfeeds(websiteId, page);
        }
    }
}