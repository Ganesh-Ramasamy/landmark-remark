using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LandmarkRemark.UseCase.Shared
{
    public class ValidationHandler<TRequest, TResponse>
        : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        private readonly IRequestHandler<TRequest, TResponse> inner;
        private readonly IValidator<TRequest>[] validators;

        public ValidationHandler(IRequestHandler<TRequest, TResponse> inner,
            IValidator<TRequest>[] validators)
        {
            this.inner = inner;
            this.validators = validators;
        }

        public TResponse Handle(TRequest request)
        {
            var context = new ValidationContext(request);

            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return inner.Handle(request);
        }
    }
}
