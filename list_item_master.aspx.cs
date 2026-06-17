using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class list_item_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
                LoadData("ALL", "ALL");
            }
        }

        private void LoadCategories()
        {
            try
            {
                DataTable dt = DBHelper.ExecuteQuery("SELECT CAT_CODE, CAT_TITLE FROM MATPASS.TBL_IWPS_CATEGORY_MASTER ORDER BY CAT_TITLE", new OracleParameter[0]);
                ddlCategory.Items.Clear();
                ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem("All Categories", "ALL"));
                foreach (DataRow row in dt.Rows)
                    ddlCategory.Items.Add(new System.Web.UI.WebControls.ListItem(row["CAT_CODE"] + " - " + row["CAT_TITLE"], row["CAT_CODE"].ToString()));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadCategories error: " + ex.Message);
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(ddlCategory.SelectedValue, ddlStatus.SelectedValue);
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(ddlCategory.SelectedValue, ddlStatus.SelectedValue);
        }

        private void LoadData(string cat, string status)
        {
            try
            {
                string sql = "SELECT im.ITEM_CODE, im.CAT_CODE, im.ITEM_DESC, im.UNIT, im.RATE, im.STATUS FROM MATPASS.TBL_IWPS_ITEM_MASTER im WHERE 1=1";
                var pList = new System.Collections.Generic.List<OracleParameter>();
                if (cat != "ALL") { sql += " AND im.CAT_CODE=:cat"; pList.Add(new OracleParameter("cat", cat)); }
                if (status != "ALL") { sql += " AND im.STATUS=:status"; pList.Add(new OracleParameter("status", status)); }
                sql += " ORDER BY im.INITIATION_DATE DESC";
                DataTable dt = DBHelper.ExecuteQuery(sql, pList.ToArray());
                gvList.DataSource = dt;
                gvList.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("List Item error: " + ex.Message);
                gvList.DataSource = new DataTable();
                gvList.DataBind();
            }
        }
    }
}
