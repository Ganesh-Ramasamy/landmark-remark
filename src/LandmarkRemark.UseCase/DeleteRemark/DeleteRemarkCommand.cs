using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.UseCase.DeleteRemark
{
    public class DeleteRemarkCommand : IRequest
    {
        public string RemarkId { get; set; }
    }
}
