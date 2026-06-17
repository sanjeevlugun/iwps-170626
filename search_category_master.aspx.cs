using System;
using System.Data;
using System.Text;

namespace IWPS
{
    public partial class search_category_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) DoSearch(); }

        DataTable DoSearch()
        {
            try
            {
                var prms = new System.Collections.Generic.List<Oracle.ManagedDataAccess.Client.OracleParameter>();
                string sql = "SELECT CAT_CODE,CAT_NAME,DEPT,STATUS,CREATED_DATE FROM MATPASS.TBL_IWPS_CATEGORY_MASTER WHERE 1=1";
                if (!string.IsNullOrEmpty(txtCode.Text.Trim())) { sql += " AND CAT_CODE LIKE :c"; prms.Add(DBHelper.P("c", "%" + txtCode.Text.Trim() + "%")); }
                if (!string.IsNullOrEmpty(txtName.Text.Trim())) { sql += " AND UPPER(CAT_NAME) LIKE :n"; prms.Add(DBHelper.P("n", "%" + txtName.Text.Trim().ToUpper() + "%")); }
                if (!string.IsNullOrEmpty(ddlStatus.SelectedValue)) { sql += " AND STATUS=:s"; prms.Add(DBHelper.P("s", ddlStatus.SelectedValue)); }
                sql += " ORDER BY CAT_CODE";
                DataTable dt = DBHelper.ExecuteQuery(sql, prms.ToArray());
                gvCat.DataSource = dt; gvCat.DataBind();
                return dt;
            }
            catch { return new DataTable(); }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { DoSearch(); }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = DoSearch();
            var sb = new StringBuilder("Code,Name,Dept,Status,Created Date\r\n");
            foreach (DataRow r in dt.Rows)
                sb.AppendLine($"{r["CAT_CODE"]},{r["CAT_NAME"]},{r["DEPT"]},{r["STATUS"]},{r["CREATED_DATE"]}");
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=categories.csv");
            Response.Write(sb.ToString());
            Response.End();
        }
    }
}
