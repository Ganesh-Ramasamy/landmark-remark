using LandmarkRemark.UseCase.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.UseCase.SearchRemark
{
    public class SearchRemarkQuery : IRequest<IList<RemarkResponse>>
    {
        public string SearchText { get; set; }
    }
}
