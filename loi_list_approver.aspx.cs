using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IWPS
{
    public partial class loi_list_approver : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPendingLois();
            }
        }

        private void LoadPendingLois()
        {
            try
            {
                string dept = SessionHelper.Dept(Session);
                string sql = @"SELECT h.LOI_NO, h.ENQ_NO, h.CONTRACT_VALUE, h.LOI_DATE, h.CREATED_BY,
                                      NVL(e.PARTY_NAME,'N/A') AS PARTY_NAME
                               FROM MATPASS.TBL_IWPS_LOI_HEADER h
                               LEFT JOIN MATPASS.TBL_IWPS_ENQ_HEADER e ON e.ENQ_NO = h.ENQ_NO
                               WHERE h.STATUS = 'DRAFT' AND h.DEPT_CODE = :p1
                               ORDER BY h.CREATED_DATE DESC";
                DataTable dt = DBHelper.ExecuteQuery(sql, DBHelper.P("p1", dept));
                gvLoi.DataSource = dt;
                gvLoi.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading LOIs: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void gvLoi_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "ApproveLoi" && e.CommandName != "RejectLoi") return;

            string loiNo = e.CommandArgument.ToString();
            string staffNo = SessionHelper.StaffNo(Session);

            // Get remarks from the row
            GridViewRow row = null;
            foreach (GridViewRow r in gvLoi.Rows)
            {
                LinkButton lb = (LinkButton)r.FindControl(e.CommandName == "ApproveLoi" ? "lbApprove" : "lbReject");
                if (lb != null && lb.CommandArgument == loiNo)
                {
                    row = r;
                    break;
                }
            }

            string remarks = "";
            if (row != null)
            {
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                if (txtRemarks != null) remarks = txtRemarks.Text.Trim();
            }

            string newStatus = e.CommandName == "ApproveLoi" ? "APPROVED" : "REJECTED";

            try
            {
                string sqlUpdate = "UPDATE MATPASS.TBL_IWPS_LOI_HEADER SET STATUS=:p1, UPDATED_BY=:p2, UPDATED_DATE=SYSTIMESTAMP WHERE LOI_NO=:p3";
                DBHelper.ExecuteNonQuery(sqlUpdate,
                    DBHelper.P("p1", newStatus),
                    DBHelper.P("p2", staffNo),
                    DBHelper.P("p3", loiNo));

                string sqlLog = @"INSERT INTO MATPASS.TBL_IWPS_LOI_PROCESS_LOGS (LOI_NO, ACTION, REMARKS, ACTION_BY, ACTION_DATE)
                                  VALUES (:p1, :p2, :p3, :p4, SYSTIMESTAMP)";
                DBHelper.ExecuteNonQuery(sqlLog,
                    DBHelper.P("p1", loiNo),
                    DBHelper.P("p2", newStatus),
                    DBHelper.P("p3", remarks),
                    DBHelper.P("p4", staffNo));

                string msg = newStatus == "APPROVED" ? "approved" : "rejected";
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", $"toastr.success('LOI {loiNo} has been {msg}.');", true);
                LoadPendingLois();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error processing LOI: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }
    }
}
