using System;
using System.Web;

namespace IWPS
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e) { }
        protected void Session_Start(object sender, EventArgs e) { }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex != null)
                System.Diagnostics.Trace.TraceError(ex.ToString());
        }
        protected void Session_End(object sender, EventArgs e) { }
        protected void Application_End(object sender, EventArgs e) { }
    }
}
