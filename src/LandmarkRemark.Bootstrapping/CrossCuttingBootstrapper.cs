using Autofac;
using LandmarkRemark.CrossCutting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.Bootstrapping
{
    class CrossCuttingBootstrapper
    {
        public void Bootstrap(ContainerBuilder builder)
        {
            builder.RegisterType<Clock>()
                .As<IClock>()
                .SingleInstance();

            builder.RegisterType<LogProvider>()
                .As<ILogProvider>()
                .SingleInstance();
        }
    }
}
