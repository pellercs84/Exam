using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Exam.DataAccessLayer;

namespace Exam.WebApplication
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The implementation of DAL/Factory can be replacable for a dummy implementation and 
        /// so the Controller layers can be tested independently from Data layer
        /// </summary>
        public static IDataProviderFactory DataProviderFactory = new DataProviderFactory();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}
