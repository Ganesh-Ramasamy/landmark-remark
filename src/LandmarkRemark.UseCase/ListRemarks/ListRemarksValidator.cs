using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.UseCase.ListRemarks
{
    class ListRemarksValidator : AbstractValidator<ListRemarksQuery>
    {
        public ListRemarksValidator()
        {
            RuleFor(query => query.Email).NotEmpty();
        }
    }
}
