using LandmarkRemark.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandmarkRemark.Web.Features.Home
{
    [Route("home")]
    public class HomeController : Controller
    {
        private IMediator mediator;

        public HomeController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [Authorize]
        [HttpGet("/")]
        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
