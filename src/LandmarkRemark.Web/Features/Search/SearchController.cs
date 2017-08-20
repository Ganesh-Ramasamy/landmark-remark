using LandmarkRemark.UseCase.SearchRemark;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandmarkRemark.Web.Features.Search
{
    
    
    [Route("search")]
    public class SearchController : Controller
    {
        
        private IMediator mediator;

        public SearchController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize]
        [HttpGet("")]
        public IActionResult Search()
        {
            return View();
        }

        
        [Authorize]
        [HttpPost("")]
        public IActionResult Search(SearchRemarkQuery query)
        {
            var response = mediator.Send(query);

            return View("Result", response.Result);
        }
    }
}
