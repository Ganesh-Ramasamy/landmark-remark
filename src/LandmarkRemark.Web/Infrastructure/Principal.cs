using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace LandmarkRemark.Web.Infrastructure
{
    public class Principal : IIdentity
    {
        /// <summary>
        /// Name is email
        /// </summary>
        public string Name { get; set; }

      
        public string AuthenticationType
        {
            get { return "Auth.01"; }
        }

        public bool IsAuthenticated { get; set; }
    }
}
