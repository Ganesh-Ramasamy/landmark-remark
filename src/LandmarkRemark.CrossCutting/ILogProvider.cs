using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.CrossCutting
{
    public interface ILogProvider
    {
        ILog For<TClass>();

        ILog For(string logName);

    }
}
