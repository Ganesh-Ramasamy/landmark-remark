using Autofac;
using LandmarkRemark.UseCase.CreateRemark;
using log4net;
using log4net.Config;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LandmarkRemark.Bootstrapping
{
    public class Bootstrapper
    {
        public void Bootstrap(ContainerBuilder builder)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            var crossCuttingBootstrapper = new CrossCuttingBootstrapper();
            crossCuttingBootstrapper.Bootstrap(builder);

            var infrastructureBootstrapper = new InfrastructureBootstrapper();
            infrastructureBootstrapper.Bootstrap(builder);


            BuildMediator(builder);
           
            var entityBoostrapper = new EntityBootstrapper();
            entityBoostrapper.Bootstrap(builder);


            var useCaseBootstrapper = new UseCaseBootstrapper();
            useCaseBootstrapper.Bootstrap(builder);

        }


        private void BuildMediator(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return c.TryResolve(t, out o) ? o : null;
                };
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

        }
    }
}
