using LandmarkRemark.UseCase.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.UseCase.CreateRemark
{
    public class CreateRemarkCommand : IRequest
    {

        public string UserEmail { get; set; }

        public string Remark { get; set; }

        public Location Location { get; set; }
    }
}
