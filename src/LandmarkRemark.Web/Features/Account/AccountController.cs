using LandmarkRemark.UseCase.GetUser;
using LandmarkRemark.Web.Infrastructure;
using MediatR;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LandmarkRemark.Web.Features.Account
{
    [Route("account")]
    public class AccountController : Controller
    {
        private IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpGet("login")]
        public IActionResult LogIn(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]         
        public IActionResult LogIn(LogInViewModel logInViewModel, string returnUrl)
        {
            returnUrl = returnUrl ?? "/";

            var authenticated = Authenticate(logInViewModel.Email, logInViewModel.Password);
            if(authenticated)
            {
                return Redirect(returnUrl);
            }

            return View();
        }

        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Authentication.SignOutAsync("Auth.01").Wait();

            return View();
        }


        private bool Authenticate(string email, string password)
        {
            var response = mediator.Send(new GetUserQuery()
            {
                Email = email
            });

            if (response.Result != null)
            {
                if (password == response.Result.Password)
                {
                    var principal = new Principal();
                    principal.Name = email;
                    principal.IsAuthenticated = true;
                    var claimsPrincipal = new ClaimsPrincipal(principal);
                    HttpContext.Authentication.SignInAsync("Auth.01", claimsPrincipal).Wait();
                    return true;
                }
            }
            return false;
        }
    }
}
