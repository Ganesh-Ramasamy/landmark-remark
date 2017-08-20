using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.UseCase.SearchRemark
{
    class SearchRemarkValidator : AbstractValidator<SearchRemarkQuery>
    {
        public SearchRemarkValidator()
        {
            RuleFor(query => query.SearchText).NotEmpty();
        }
    }
}
