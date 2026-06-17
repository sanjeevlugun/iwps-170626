using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IWPS
{
    public partial class print_loi_list : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLois();
            }
        }

        private void LoadLois()
        {
            try
            {
                string dept = SessionHelper.Dept(Session);
                string loiFilter = txtFilterLoiNo.Text.Trim();
                string statusFilter = ddlFilterStatus.SelectedValue;

                string sql = @"SELECT LOI_NO, ENQ_NO, CONTRACT_VALUE, STATUS, LOI_DATE, CREATED_BY
                               FROM MATPASS.TBL_IWPS_LOI_HEADER
                               WHERE DEPT_CODE = :p1
                               AND (:p2 IS NULL OR UPPER(LOI_NO) LIKE UPPER(:p2))
                               AND (:p3 IS NULL OR STATUS = :p3)
                               ORDER BY CREATED_DATE DESC";

                DataTable dt = DBHelper.ExecuteQuery(sql,
                    DBHelper.P("p1", dept),
                    DBHelper.P("p2", string.IsNullOrEmpty(loiFilter) ? null : "%" + loiFilter + "%"),
                    DBHelper.P("p3", string.IsNullOrEmpty(statusFilter) ? null : statusFilter));

                gvLoi.DataSource = dt;
                gvLoi.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err", $"toastr.error('Error loading LOIs: {ex.Message.Replace("'", "\\'")}');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvLoi.PageIndex = 0;
            LoadLois();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtFilterLoiNo.Text = "";
            ddlFilterStatus.SelectedIndex = 0;
            gvLoi.PageIndex = 0;
            LoadLois();
        }

        protected void gvLoi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLoi.PageIndex = e.NewPageIndex;
            LoadLois();
        }

        protected string GetStatusBadge(string status)
        {
            switch (status?.ToUpper())
            {
                case "DRAFT": return "badge-draft";
                case "APPROVED": return "badge-approved";
                case "REJECTED": return "badge-rejected";
                default: return "badge-draft";
            }
        }
    }
}
