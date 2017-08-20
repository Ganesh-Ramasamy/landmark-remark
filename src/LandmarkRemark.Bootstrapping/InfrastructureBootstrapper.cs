using Autofac;
using LandmarkRemark.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.Bootstrapping
{
    class InfrastructureBootstrapper
    {
        public void Bootstrap(ContainerBuilder builder)
        {
            builder.RegisterType<ConnectionFactory>()
              .As<IConnectionFactory>()
              .SingleInstance();
        }
    }
}
