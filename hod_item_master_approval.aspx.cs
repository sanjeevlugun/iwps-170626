using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class hod_item_master_approval : System.Web.UI.Page
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
                    "SELECT ITEM_CODE, ITEM_CATEGORY, ITEM_DESCRIPTION, ITEM_UNIT, ITEM_RATE, STATUS " +
                    "FROM MATPASS.TBL_IWPS_ITEM_MASTER WHERE STATUS='INITIATED' ORDER BY TRANSACTION_DT DESC");
                gvList.DataSource = dt;
                gvList.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("HOD Item Approval LoadData error: " + ex.Message);
                gvList.DataSource = new DataTable();
                gvList.DataBind();
            }
        }

        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "APPROVE" && e.CommandName != "REJECT") return;

            string itemCode = e.CommandArgument.ToString();
            string staffNo = SessionHelper.StaffNo(Session);
            string newStatus = e.CommandName == "APPROVE" ? "ACTIVE" : "REJECTED";
            string remarks = GetRemarks(itemCode);

            try
            {
                DBHelper.ExecuteNonQuery(
                    "UPDATE MATPASS.TBL_IWPS_ITEM_MASTER SET STATUS=:s WHERE ITEM_CODE=:c",
                    DBHelper.P(":s", newStatus), DBHelper.P(":c", itemCode));

                DBHelper.ExecuteNonQuery(
                    "INSERT INTO MATPASS.TBL_IWPS_ITEM_MASTER_LOGS " +
                    "(ITEM_CODE, STAFFNO, STATUS, TRANSACTION_DT, PCNAME_IPADDRESS, LOGIN_STAFFNO, REMARKS) " +
                    "VALUES (:c, :sno, :s, SYSTIMESTAMP, :ip, :sno, :r)",
                    DBHelper.P(":c", itemCode),
                    DBHelper.P(":sno", staffNo),
                    DBHelper.P(":s", newStatus),
                    DBHelper.P(":ip", Request.UserHostAddress),
                    DBHelper.P(":r", remarks));

                ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                    $"toastr.success('Item {itemCode} {newStatus}.');", true);
                LoadData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "toast",
                    "toastr.error('Error: " + ex.Message.Replace("'", "\\'") + "');", true);
            }
        }

        private string GetRemarks(string itemCode)
        {
            foreach (GridViewRow row in gvList.Rows)
            {
                if (row.RowType != DataControlRowType.DataRow) continue;
                if (row.Cells[0].Text.Trim() == itemCode)
                {
                    var txt = row.FindControl("txtRemarks") as TextBox;
                    return txt != null ? txt.Text.Trim() : "";
                }
            }
            return "";
        }
    }
}
