using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LandmarkRemark.CrossCutting
{
    class LogProvider : ILogProvider
    {
        private static readonly Assembly entryAssembly = Assembly.GetEntryAssembly();

        public ILog For<TClass>()
        {
            var log4netLog = LogManager.GetLogger(typeof(TClass));

            var log = new Log(log4netLog);
            return log;
        }

        public ILog For(string logName)
        {
            var log4netLog = LogManager.GetLogger(entryAssembly, logName);

            var log = new Log(log4netLog);
            return log;
        }
    }
}
