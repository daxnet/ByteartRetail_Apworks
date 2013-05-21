using System;
using Apworks.Application;
using Apworks.Config.Fluent;
using ByteartRetail.Application;
using ByteartRetail.Domain.Repositories.EntityFramework;

namespace ByteartRetail.Services.WCF
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ByteartRetailDbContextInitailizer.Initialize();
            ApplicationService.Initialize();
            log4net.Config.XmlConfigurator.Configure();
            AppRuntime.Instance
                      .ConfigureApworks()
                      .UsingUnityContainerWithDefaultSettings(true, "unity")
                      .Create().Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}