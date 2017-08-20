using LandmarkRemark.UseCase.GetUser;
using LandmarkRemark.UseCase.ListRemarks;
using LandmarkRemark.UseCase.SearchRemark;
using LandmarkRemark.Web.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandmarkRemark.Web.Features.MyRemarks
{
    [Route("myremarks")]
    public class MyRemarksController : Controller
    {
        private IMediator mediator;

        public MyRemarksController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [Authorize]      
        [HttpGet("list")]
        public IActionResult List()
        {           
            var identity = User.Identity;
            var response = mediator.Send(new ListRemarksQuery()
            {
                Email = identity.Name,
                MyRemarks = true
            });

            return View(response.Result);
        }

    }
}
