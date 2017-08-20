using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.CrossCutting
{
    public interface ILog
    {
        void Debug(string log);

        void Debug(string log, Exception ex);


        void Info(string log);

        void Info(string log, Exception ex);

        void Error(string log);

        void Error(string log, Exception ex);
    }
}
