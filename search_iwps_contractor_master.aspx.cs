using System;

namespace IWPS
{
    public partial class search_iwps_contractor_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) Search("", "", ""); }

        void Search(string code, string name, string status)
        {
            try
            {
                var prms = new System.Collections.Generic.List<Oracle.ManagedDataAccess.Client.OracleParameter>();
                string sql = "SELECT CONTRACTOR_CODE,CONTRACTOR_NAME,CONTRACTOR_EMAIL,CONTRACTOR_MOBILE,STATUS FROM MATPASS.TBL_IWPS_CONTRACTOR_MASTER WHERE 1=1";
                if (!string.IsNullOrEmpty(code)) { sql += " AND CONTRACTOR_CODE LIKE :c"; prms.Add(DBHelper.P("c", "%" + code + "%")); }
                if (!string.IsNullOrEmpty(name)) { sql += " AND UPPER(CONTRACTOR_NAME) LIKE :n"; prms.Add(DBHelper.P("n", "%" + name.ToUpper() + "%")); }
                if (!string.IsNullOrEmpty(status)) { sql += " AND STATUS=:s"; prms.Add(DBHelper.P("s", status)); }
                sql += " ORDER BY CONTRACTOR_NAME";
                gvCon.DataSource = DBHelper.ExecuteQuery(sql, prms.ToArray());
                gvCon.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { Search(txtCode.Text.Trim(), txtName.Text.Trim(), ddlStatus.SelectedValue); }
    }
}
