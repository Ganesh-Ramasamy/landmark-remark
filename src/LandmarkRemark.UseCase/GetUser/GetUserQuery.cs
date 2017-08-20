using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.UseCase.GetUser
{
    public class GetUserQuery : IRequest<User>
    {
        public string Email { get; set; }
    }
}
