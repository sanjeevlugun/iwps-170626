using System;
using System.Data;
using System.Web.UI.WebControls;

namespace IWPS
{
    public partial class app_wo_amend_req : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) Load(); }

        void Load()
        {
            try
            {
                gvAmend.DataSource = DBHelper.ExecuteQuery(
                    "SELECT AMEND_ID,WO_NO,AMEND_REASON,NEW_END_DATE,NEW_CONTRACT_VALUE,STATUS FROM MATPASS.TBL_IWPS_WO_AMENDMENT WHERE STATUS='INITIATED' ORDER BY CREATED_DATE DESC");
                gvAmend.DataBind();
            }
            catch { }
        }

        protected void gvAmend_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string amendId = e.CommandArgument.ToString();
            string remarks = "";
            foreach (GridViewRow r in gvAmend.Rows)
            {
                if (gvAmend.DataKeys[r.RowIndex].Value.ToString() == amendId)
                { remarks = ((TextBox)r.FindControl("txtRemarks"))?.Text ?? ""; break; }
            }
            string newStatus = e.CommandName == "APR" ? "APPROVED" : "REJECTED";
            try
            {
                DataTable dt = DBHelper.ExecuteQuery("SELECT WO_NO,NEW_END_DATE,NEW_CONTRACT_VALUE FROM MATPASS.TBL_IWPS_WO_AMENDMENT WHERE AMEND_ID=:id",
                    DBHelper.P("id", amendId));
                DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_WO_AMENDMENT SET STATUS=:s WHERE AMEND_ID=:id",
                    DBHelper.P("s", newStatus), DBHelper.P("id", amendId));
                if (e.CommandName == "APR" && dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    DBHelper.ExecuteNonQuery("UPDATE MATPASS.TBL_IWPS_WO_HEADER SET WO_END_DATE=:ed,CONTRACT_VALUE=NVL(:cv,CONTRACT_VALUE) WHERE WO_NO=:w",
                        DBHelper.PDate("ed", row["NEW_END_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["NEW_END_DATE"])),
                        DBHelper.P("cv", row["NEW_CONTRACT_VALUE"]),
                        DBHelper.P("w", row["WO_NO"].ToString()));
                }
                Load();
                ClientScript.RegisterStartupScript(GetType(), "ok", "toastr.success('Amendment " + newStatus + ".');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "err", "toastr.error('" + ex.Message.Replace("'","") + "');", true);
            }
        }
    }
}
