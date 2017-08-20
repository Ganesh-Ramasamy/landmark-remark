using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.UseCase.CreateOrUpdateUser
{
    public class CreateOrUpdateUserCommand : IRequest
    {
        public string Email { get; set; }

        public string Name { get; set; }
                
    }
}
