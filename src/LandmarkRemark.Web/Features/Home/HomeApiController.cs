using LandmarkRemark.UseCase.CreateRemark;
using LandmarkRemark.UseCase.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandmarkRemark.Web.Features.Home
{

    [Route("api/remarks")]
    public class HomeApiController : Controller
    {
        private readonly IMediator mediator;


        public HomeApiController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [Authorize]
        [HttpPost("")]
        public IActionResult Create([FromBody]CreateRemarkCommand command)
        {
            var email = User.Identity.Name;
            command.UserEmail = email;
            
            mediator.Send(command);

            return Ok();
        }

    }
}
