using System;
using System.Data;
using System.Web;
using System.Web.Security;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionHelper.IsLoggedIn(Session))
                {
                    string role = SessionHelper.Role(Session);
                    Response.Redirect(role == "APP" ? "~/app_home.aspx" : "~/Nor_home.aspx");
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string staffNo = txtStaffNo.Text.Trim().ToUpper();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(staffNo) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Please enter Staff No and Password.";
                lblError.Visible = true;
                return;
            }

            try
            {
                string sql = "SELECT STAFFNO, STAFF_NAME, DEPT, ROLE FROM MATPASS.TBL_IWPS_USERS WHERE STAFFNO=:sno AND PASSWORD_HASH=:pwd AND STATUS='ACTIVE'";
                var p = new OracleParameter[] {
                    new OracleParameter("sno", staffNo),
                    new OracleParameter("pwd", password)
                };
                DataTable dt = DBHelper.ExecuteQuery(sql, p);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    SessionHelper.SetUser(Session, row["STAFFNO"].ToString(), row["STAFF_NAME"].ToString(), row["DEPT"].ToString(), row["ROLE"].ToString());
                    string role = row["ROLE"].ToString();
                    FormsAuthentication.SetAuthCookie(staffNo, false);
                    Response.Redirect(role == "APP" ? "~/app_home.aspx" : "~/Nor_home.aspx");
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Login DB error: " + ex.Message);
            }

            // Fallback demo credentials
            if (staffNo == "STAFF01" && password == "bhel123")
            {
                SessionHelper.SetUser(Session, "STAFF01", "Demo NOR User", "CIVIL", "NOR");
                FormsAuthentication.SetAuthCookie("STAFF01", false);
                Response.Redirect("~/Nor_home.aspx");
                return;
            }
            else if (staffNo == "APP01" && password == "bhel123")
            {
                SessionHelper.SetUser(Session, "APP01", "Demo APP User", "CIVIL", "APP");
                FormsAuthentication.SetAuthCookie("APP01", false);
                Response.Redirect("~/app_home.aspx");
                return;
            }

            lblError.Text = "Invalid credentials. Please try again.";
            lblError.Visible = true;
        }
    }
}
