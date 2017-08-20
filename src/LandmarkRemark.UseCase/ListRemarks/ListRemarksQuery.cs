using LandmarkRemark.UseCase.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.UseCase.ListRemarks
{
    public class ListRemarksQuery : IRequest<IList<RemarkResponse>>
    {
        public string Email { get; set; }

        public bool MyRemarks { get; set; }
    }
}
