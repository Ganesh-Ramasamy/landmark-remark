using Autofac;
using LandmarkRemark.UseCase.CreateRemark;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using LandmarkRemark.UseCase.CreateOrUpdateUser;
using LandmarkRemark.UseCase.GetUser;
using LandmarkRemark.UseCase.DeleteRemark;
using LandmarkRemark.UseCase.SearchRemark;
using LandmarkRemark.UseCase.Shared;
using System.Linq;
using Autofac.Core;
using FluentValidation;

namespace LandmarkRemark.Bootstrapping
{
    class UseCaseBootstrapper
    {
        public void Bootstrap(ContainerBuilder builder)
        {
            //Validation(builder);

            var useCaseAssembly = typeof(CreateRemarkCommand).GetTypeInfo().Assembly;


            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<>),
                typeof(IRequestHandler<,>),
                typeof(IAsyncRequestHandler<,>),
                typeof(ICancellableAsyncRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IAsyncNotificationHandler<>),
                typeof(ICancellableAsyncNotificationHandler<>)                
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(useCaseAssembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
               
            }


            //builder.RegisterType<ValidationBehavior<SearchRemarkQuery, IList<RemarkResponse>>>()
            //    .As<IPipelineBehavior<SearchRemarkQuery, IList<RemarkResponse>>>()
            //    ;//.SingleInstance();

            builder.RegisterGeneric(typeof(ValidationBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.RegisterAssemblyTypes(useCaseAssembly)
               .As(type => type.GetInterfaces()
                   .Where(interfacetype => interfacetype.IsClosedTypeOf(typeof(IValidator<>))));
        }


        private static void Validation(ContainerBuilder builder)
        { 
            builder.RegisterAssemblyTypes(typeof(SearchRemarkQuery).GetTypeInfo().Assembly)
                .As(type => type.GetInterfaces()
                    .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                    .Select(interfaceType => new KeyedService("handler", interfaceType)));

            //builder.RegisterGenericDecorator(typeof(AsyncValidationHandler<,>), typeof(IAsyncRequestHandler<,>), fromKey: "async-error-handler");
            builder.RegisterGenericDecorator(
                typeof(ValidationHandler<,>), 
                typeof(IRequestHandler<,>), fromKey: "handler");

            builder.RegisterAssemblyTypes(typeof(SearchRemarkQuery).GetTypeInfo().Assembly)
                .As(type => type.GetInterfaces()
                    .Where(interfacetype => interfacetype.IsClosedTypeOf(typeof(IValidator<>))));
        }
    }
}
