using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IWPS
{
    public partial class pending_app_wo_list : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadWos();
        }

        private void LoadWos()
        {
            try
            {
                string dept = SessionHelper.Dept(Session);
                string sql = @"SELECT w.WO_NO, w.ENQ_NO, w.LOI_NO, w.CONTRACT_VALUE, w.WO_DATE, w.CREATED_BY,
                                      NVL(e.PARTY_NAME,'N/A') AS PARTY_NAME
                               FROM MATPASS.TBL_IWPS_WO_HEADER w
                               LEFT JOIN MATPASS.TBL_IWPS_ENQ_HEADER e ON e.ENQ_NO = w.ENQ_NO
                               WHERE w.STATUS = 'FORWARDED' AND w.DEPT_CODE = :p1
                               ORDER BY w.CREATED_DATE DESC";
                DataTable dt = DBHelper.ExecuteQuery(sql, DBHelper.P("p1", dept));
                gvWo.DataSource = dt;
                gvWo.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading work orders: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void gvWo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "ApproveWo" && e.CommandName != "RejectWo") return;

            string woNo = e.CommandArgument.ToString();
            string staffNo = SessionHelper.StaffNo(Session);

            GridViewRow row = null;
            foreach (GridViewRow r in gvWo.Rows)
            {
                string ctlName = e.CommandName == "ApproveWo" ? "lbApprove" : "lbReject";
                LinkButton lb = (LinkButton)r.FindControl(ctlName);
                if (lb != null && lb.CommandArgument == woNo) { row = r; break; }
            }

            string remarks = "";
            if (row != null)
            {
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                if (txtRemarks != null) remarks = txtRemarks.Text.Trim();
            }

            string newStatus = e.CommandName == "ApproveWo" ? "APPROVED" : "REJECTED";
            string action = e.CommandName == "ApproveWo" ? "APPROVED BY HOD" : "REJECTED BY HOD";

            try
            {
                DBHelper.ExecuteNonQuery(
                    "UPDATE MATPASS.TBL_IWPS_WO_HEADER SET STATUS=:p1, UPDATED_BY=:p2, UPDATED_DATE=SYSTIMESTAMP WHERE WO_NO=:p3",
                    DBHelper.P("p1", newStatus), DBHelper.P("p2", staffNo), DBHelper.P("p3", woNo));

                DBHelper.ExecuteNonQuery(
                    "INSERT INTO MATPASS.TBL_IWPS_WO_PROCESS_LOGS (WO_NO, ACTION, REMARKS, ACTION_BY, ACTION_DATE) VALUES (:p1,:p2,:p3,:p4,SYSTIMESTAMP)",
                    DBHelper.P("p1", woNo), DBHelper.P("p2", action), DBHelper.P("p3", remarks), DBHelper.P("p4", staffNo));

                string msg = e.CommandName == "ApproveWo" ? "approved" : "rejected";
                ScriptManager.RegisterStartupScript(this, GetType(), "msg", $"toastr.success('WO {woNo} has been {msg}.');", true);
                LoadWos();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error processing WO: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void gvWo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWo.PageIndex = e.NewPageIndex;
            LoadWos();
        }
    }
}
