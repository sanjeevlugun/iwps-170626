using System;
using System.Data;

namespace IWPS
{
    public partial class deptwise_category_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DataTable dt = DBHelper.ExecuteQuery("SELECT DISTINCT DEPT FROM MATPASS.TBL_IWPS_CATEGORY_MASTER ORDER BY DEPT");
                    foreach (DataRow r in dt.Rows)
                        ddlDept.Items.Add(new System.Web.UI.WebControls.ListItem(r["DEPT"].ToString()));
                }
                catch { }
            }
        }

        protected void ddlDept_Changed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlDept.SelectedValue)) return;
            try
            {
                gvCat.DataSource = DBHelper.ExecuteQuery(@"SELECT c.CAT_CODE,c.CAT_NAME,c.STATUS,c.CREATED_DATE,
                    NVL((SELECT COUNT(*) FROM MATPASS.TBL_IWPS_ITEM_MASTER i WHERE i.CAT_CODE=c.CAT_CODE),0) ITEM_COUNT
                    FROM MATPASS.TBL_IWPS_CATEGORY_MASTER c WHERE c.DEPT=:d ORDER BY c.CAT_CODE",
                    DBHelper.P("d", ddlDept.SelectedValue));
                gvCat.DataBind();
            }
            catch { }
        }
    }
}
