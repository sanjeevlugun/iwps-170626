using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class wcd_category_master_approval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = DBHelper.ExecuteQuery(
                    "SELECT CATEGORY_CODE, CATEGORY_TITLE, TYPE_OF_WORK, DEPT, STATUS " +
                    "FROM MATPASS.TBL_IWPS_CATEGORY_MASTER WHERE STATUS='FORWARDED' ORDER BY TRANSACTION_DT DESC");
                gvList.DataSource = dt;
                gvList.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("WCD Cat Approval LoadData error: " + ex.Message);
                gvList.DataSource = new DataTable();
                gvList.DataBind();
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "APPROVE" && e.CommandName != "REJECT") return;

            string catCode = e.CommandArgument.ToString();
            string staffNo = SessionHelper.StaffNo(Session);
            string newStatus = e.CommandName == "APPROVE" ? "ACTIVE" : "REJECTED";
            string remarks = GetRemarks(catCode);

            try
            {
                DBHelper.ExecuteNonQuery(
                    "UPDATE MATPASS.TBL_IWPS_CATEGORY_MASTER SET STATUS=:s WHERE CATEGORY_CODE=:c",
                    DBHelper.P(":s", newStatus), DBHelper.P(":c", catCode));

                DBHelper.ExecuteNonQuery(
                    "INSERT INTO MATPASS.TBL_IWPS_CATEGORY_MASTER_LOGS " +
                    "(CATEGORY_CODE, STAFFNO, STATUS, TRANSACTION_DT, PCNAME_IPADDRESS, LOGIN_STAFFNO, REMARKS) " +
                    "VALUES (:c, :sno, :s, SYSTIMESTAMP, :ip, :sno, :r)",
                    DBHelper.P(":c", catCode),
                    DBHelper.P(":sno", staffNo),
                    DBHelper.P(":s", newStatus),
                    DBHelper.P(":ip", Request.UserHostAddress),
                    DBHelper.P(":r", remarks));

                ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                    $"toastr.success('Category {catCode} {newStatus}.');", true);
                LoadData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                    "toastr.error('Error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        private string GetRemarks(string catCode)
        {
            foreach (GridViewRow row in gvList.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow) continue;
                if (row.Cells[0].Text.Trim() == catCode)
                {
                    var txt = row.FindControl("txtRemarks") as TextBox;
                    return txt != null ? txt.Text.Trim() : "";
                }
            }
            return "";
        }
    }
}
