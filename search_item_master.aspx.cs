using System;
using System.Data;
using System.Text;

namespace IWPS
{
    public partial class search_item_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DataTable dt = DBHelper.ExecuteQuery("SELECT CAT_CODE,CAT_NAME FROM MATPASS.TBL_IWPS_CATEGORY_MASTER WHERE STATUS='ACTIVE' ORDER BY CAT_NAME");
                    foreach (DataRow r in dt.Rows)
                        ddlCat.Items.Add(new System.Web.UI.WebControls.ListItem(r["CAT_CODE"] + " - " + r["CAT_NAME"], r["CAT_CODE"].ToString()));
                }
                catch { }
                DoSearch();
            }
        }

        DataTable DoSearch()
        {
            try
            {
                var prms = new System.Collections.Generic.List<Oracle.ManagedDataAccess.Client.OracleParameter>();
                string sql = "SELECT ITEM_CODE,ITEM_NAME,CAT_CODE,UOM,STATUS,CREATED_DATE FROM MATPASS.TBL_IWPS_ITEM_MASTER WHERE 1=1";
                if (!string.IsNullOrEmpty(txtCode.Text.Trim())) { sql += " AND ITEM_CODE LIKE :c"; prms.Add(DBHelper.P("c", "%" + txtCode.Text.Trim() + "%")); }
                if (!string.IsNullOrEmpty(txtName.Text.Trim())) { sql += " AND UPPER(ITEM_NAME) LIKE :n"; prms.Add(DBHelper.P("n", "%" + txtName.Text.Trim().ToUpper() + "%")); }
                if (!string.IsNullOrEmpty(ddlCat.SelectedValue)) { sql += " AND CAT_CODE=:k"; prms.Add(DBHelper.P("k", ddlCat.SelectedValue)); }
                if (!string.IsNullOrEmpty(ddlStatus.SelectedValue)) { sql += " AND STATUS=:s"; prms.Add(DBHelper.P("s", ddlStatus.SelectedValue)); }
                sql += " ORDER BY ITEM_CODE";
                DataTable dt = DBHelper.ExecuteQuery(sql, prms.ToArray());
                gvItems.DataSource = dt;
                gvItems.DataBind();
                return dt;
            }
            catch { return new DataTable(); }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { DoSearch(); }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = DoSearch();
            var sb = new StringBuilder("Item Code,Item Name,Category,UOM,Status,Created Date\r\n");
            foreach (DataRow r in dt.Rows)
                sb.AppendLine($"{r["ITEM_CODE"]},{r["ITEM_NAME"]},{r["CAT_CODE"]},{r["UOM"]},{r["STATUS"]},{r["CREATED_DATE"]}");
            Response.ContentType = "text/csv";
            Response.AddHeader("Content-Disposition", "attachment;filename=items.csv");
            Response.Write(sb.ToString());
            Response.End();
        }
    }
}
