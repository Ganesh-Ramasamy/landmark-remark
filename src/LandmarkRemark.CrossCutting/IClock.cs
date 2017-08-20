using System;

namespace LandmarkRemark.CrossCutting
{
    public interface IClock
    {
        DateTime UtcNow { get; }
    }
}
