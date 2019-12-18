﻿using LeagueOfNews.Model;
using LeagueOfNews.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeagueOfNews.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppConfigController : ControllerBase
    {
        private IAppConfigService _appConfigService;
        public AppConfigController(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }

        [HttpGet]
        public AppConfig Get()
        {
            return _appConfigService.AppConfig;
        }
    }
}