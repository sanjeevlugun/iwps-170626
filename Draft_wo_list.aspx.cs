using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IWPS
{
    public partial class Draft_wo_list : Page
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
                string statusFilter = ddlFilterStatus.SelectedValue;

                string sql = @"SELECT WO_NO, ENQ_NO, LOI_NO, CONTRACT_VALUE, STATUS, WO_DATE
                               FROM MATPASS.TBL_IWPS_WO_HEADER
                               WHERE DEPT_CODE = :p1
                               AND STATUS IN ('DRAFT','INITIATED','REJECTED')
                               AND (:p2 IS NULL OR STATUS = :p2)
                               ORDER BY CREATED_DATE DESC";

                DataTable dt = DBHelper.ExecuteQuery(sql,
                    DBHelper.P("p1", dept),
                    DBHelper.P("p2", string.IsNullOrEmpty(statusFilter) ? null : statusFilter));

                gvWo.DataSource = dt;
                gvWo.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading work orders: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvWo.PageIndex = 0;
            LoadWos();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlFilterStatus.SelectedIndex = 0;
            gvWo.PageIndex = 0;
            LoadWos();
        }

        protected void gvWo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWo.PageIndex = e.NewPageIndex;
            LoadWos();
        }

        protected void gvWo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteWo")
            {
                string woNo = e.CommandArgument.ToString();
                try
                {
                    // Only delete if DRAFT
                    object statusObj = DBHelper.ExecuteScalar("SELECT STATUS FROM MATPASS.TBL_IWPS_WO_HEADER WHERE WO_NO=:p1", DBHelper.P("p1", woNo));
                    if (statusObj?.ToString() != "DRAFT")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "err", "toastr.error('Only DRAFT work orders can be deleted.');", true);
                        return;
                    }

                    DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_WO_NOTES WHERE WO_NO=:p1", DBHelper.P("p1", woNo));
                    DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_WO_ITEMS WHERE WO_NO=:p1", DBHelper.P("p1", woNo));
                    DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_WO_PROCESS_LOGS WHERE WO_NO=:p1", DBHelper.P("p1", woNo));
                    DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_WO_HEADER WHERE WO_NO=:p1", DBHelper.P("p1", woNo));

                    ScriptManager.RegisterStartupScript(this, GetType(), "msg", $"toastr.success('Work Order {woNo} deleted.');", true);
                    LoadWos();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error deleting: {ex.Message.Replace("'", "\\'")}');", true);
                }
            }
        }
    }
}
