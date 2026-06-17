using System;

namespace IWPS
{
    public partial class search_clc_contractor_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) Search("", ""); }

        void Search(string clcid, string name)
        {
            try
            {
                var prms = new System.Collections.Generic.List<Oracle.ManagedDataAccess.Client.OracleParameter>();
                string sql = "SELECT CLC_ID,PARTY_NAME,PARTY_EMAIL,PARTY_MOBILE,STATUS FROM MATPASS.TBL_IWPS_CLC_CONTRACTOR_MASTER WHERE 1=1";
                if (!string.IsNullOrEmpty(clcid)) { sql += " AND CLC_ID LIKE :c"; prms.Add(DBHelper.P("c", "%" + clcid + "%")); }
                if (!string.IsNullOrEmpty(name)) { sql += " AND UPPER(PARTY_NAME) LIKE :n"; prms.Add(DBHelper.P("n", "%" + name.ToUpper() + "%")); }
                sql += " ORDER BY PARTY_NAME";
                gvCLC.DataSource = DBHelper.ExecuteQuery(sql, prms.ToArray());
                gvCLC.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { Search(txtCLCId.Text.Trim(), txtName.Text.Trim()); }
    }
}
