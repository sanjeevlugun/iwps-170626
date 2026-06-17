using System;
namespace IWPS {
    public partial class list_indent_print : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) Load(""); }
        void Load(string q) {
            string sql = "SELECT INDENT_NO,INDENT_DATE,TITLE,INDENT_DEPT,STATUS FROM MATPASS.TBL_IWPS_INDENT_HEADER";
            try {
                if (!string.IsNullOrEmpty(q)) { gvList.DataSource = DBHelper.ExecuteQuery(sql + " WHERE INDENT_NO LIKE :q ORDER BY INDENT_DATE DESC", DBHelper.P(":q","%"+q+"%")); }
                else { gvList.DataSource = DBHelper.ExecuteQuery(sql + " ORDER BY INDENT_DATE DESC FETCH FIRST 50 ROWS ONLY"); }
                gvList.DataBind();
            } catch { }
        }
        protected void btnSearch_Click(object sender, EventArgs e) { Load(txtSearch.Text.Trim()); }
    }
}
