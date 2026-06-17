using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace IWPS
{
    public partial class iwps : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!SessionHelper.IsLoggedIn(Session))
            {
                Response.Redirect("~/login.aspx");
                return;
            }

            string staffNo = SessionHelper.StaffNo(Session);
            string userName = SessionHelper.UserName(Session);
            string dept = SessionHelper.Dept(Session);
            string role = SessionHelper.Role(Session);

            mm_lblmsg.Text = string.Format("{0} | {1} | {2} | {3}",
                staffNo, userName, dept, DateTime.Now.ToString("dd-MMM-yyyy"));

            if (role == "APP")
            {
                app_menu.Visible = true;
                nor_menu.Visible = false;
            }
            else
            {
                app_menu.Visible = false;
                nor_menu.Visible = true;
            }
        }

        protected void App_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Text == "LOGOUT")
            {
                SessionHelper.Clear(Session);
                FormsAuthentication.SignOut();
                Response.Redirect("~/login.aspx");
            }
        }

        protected void nor_menu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Text == "LOGOUT")
            {
                SessionHelper.Clear(Session);
                FormsAuthentication.SignOut();
                Response.Redirect("~/login.aspx");
            }
        }
    }
}
