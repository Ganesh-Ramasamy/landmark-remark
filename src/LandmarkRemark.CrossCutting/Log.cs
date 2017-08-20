using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.CrossCutting
{
    class Log : ILog
    {

        private readonly log4net.ILog log4netLog;

        public Log(log4net.ILog log4netLog)
        {
            this.log4netLog = log4netLog;
        }

        public void Debug(string log)
        {
            log4netLog.Debug(log); 
        }

        public void Debug(string log, Exception ex)
        {
            log4netLog.Debug(log, ex);
        }

        public void Error(string log)
        {
            log4netLog.Error(log);
        }

        public void Error(string log, Exception ex)
        {
            log4netLog.Error(log, ex); 
        }

        public void Info(string log)
        {
            log4netLog.Info(log);
        }

        public void Info(string log, Exception ex)
        {
            log4netLog.Info(log, ex);
        }
    }
}
