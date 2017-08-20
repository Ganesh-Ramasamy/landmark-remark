using LandmarkRemark.UseCase.GetUser;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LandmarkRemark.Web.Features.Search
{
    [Route("api/remarks")]
    public class SearchApiController : Controller
    {
        [HttpGet("")]
        public ActionResult Get(GetUserQuery query)
        {
            var user = new User();
            user.Id = Guid.NewGuid().ToString();
            user.Email = query.Email;
            user.Name = "tien huat";
            return Json(user);
        }
    }
}
