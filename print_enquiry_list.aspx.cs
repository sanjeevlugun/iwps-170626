using System;
using System.Data;
namespace IWPS {
    public partial class print_enquiry_list : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) LoadGrid();
        }
        void LoadGrid() {
            try {
                string sql = "SELECT ENQ_NO, NVL(INDENT_NO,'-') INDENT_NO, ENQ_TYPE, STATUS, CREATED_DATE FROM MATPASS.TBL_IWPS_ENQUIRY_HEADER WHERE 1=1";
                var ps = new System.Collections.Generic.List<Oracle.ManagedDataAccess.Client.OracleParameter>();
                if (!string.IsNullOrWhiteSpace(txtEnqNo.Text)) { sql += " AND ENQ_NO LIKE :enq"; ps.Add(DBHelper.P("enq", "%" + txtEnqNo.Text.Trim() + "%")); }
                if (ddlStatus.SelectedValue != "") { sql += " AND STATUS=:st"; ps.Add(DBHelper.P("st", ddlStatus.SelectedValue)); }
                sql += " ORDER BY CREATED_DATE DESC";
                gvEnquiries.DataSource = DBHelper.ExecuteQuery(sql, ps.ToArray());
                gvEnquiries.DataBind();
            } catch (Exception ex) {
                ClientScript.RegisterStartupScript(GetType(), "err", $"toastr.error('{ex.Message.Replace("'","\\'")}');", true);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e) { LoadGrid(); }
    }
}
