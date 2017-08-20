using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.CrossCutting
{
    class Clock : IClock
    {
        public DateTime UtcNow
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
