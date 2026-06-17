using System; using System.Web.UI.WebControls;
namespace IWPS {
    public partial class draft_indent_list : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) Load(); }
        void Load() {
            string s = ddlStatus.SelectedValue;
            string sql = "SELECT INDENT_NO,INDENT_DATE,TITLE,INDENT_DEPT,CATEGORY_CODE,TOTAL_ESTIMATE,STATUS FROM MATPASS.TBL_IWPS_INDENT_HEADER";
            if (!string.IsNullOrEmpty(s)) sql += " WHERE STATUS=:s";
            sql += " ORDER BY INDENT_DATE DESC";
            try {
                gvList.DataSource = string.IsNullOrEmpty(s) ? DBHelper.ExecuteQuery(sql) : DBHelper.ExecuteQuery(sql, DBHelper.P(":s",s));
                gvList.DataBind();
            } catch { }
        }
        protected void ddlStatus_Changed(object sender, EventArgs e) { Load(); }
        protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e) {
            if (e.CommandName == "DEL") {
                try {
                    DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_INDENT_ITEM WHERE INDENT_NO=:n", DBHelper.P(":n", e.CommandArgument));
                    DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_INDENT_DOCUMENT WHERE INDENT_NO=:n", DBHelper.P(":n", e.CommandArgument));
                    DBHelper.ExecuteNonQuery("DELETE FROM MATPASS.TBL_IWPS_INDENT_HEADER WHERE INDENT_NO=:n AND STATUS='DRAFT'", DBHelper.P(":n", e.CommandArgument));
                    lblMsg.Text = "Indent deleted.";
                } catch (Exception ex) { lblMsg.Text = "Error: " + ex.Message; }
                Load();
            }
        }
    }
}
