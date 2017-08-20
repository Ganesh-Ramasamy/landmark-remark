using System;
using System.Collections.Generic;
using System.Text;

namespace LandmarkRemark.UseCase.Shared
{
    public class RemarkResponse
    {
        public string RemarkId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

       

        public string Remark { get; set; }

        public Location Location { get; set; }

    }
}
