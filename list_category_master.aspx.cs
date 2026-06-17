using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class list_category_master : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) LoadData("ALL");
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData(ddlStatus.SelectedValue);
        }

        private void LoadData(string status)
        {
            try
            {
                string sql = "SELECT CAT_CODE, CAT_TITLE, TYPE_OF_WORK, WORK_SERVICE_TYPE, DEPT, STATUS FROM MATPASS.TBL_IWPS_CATEGORY_MASTER";
                OracleParameter[] p;
                if (status != "ALL")
                {
                    sql += " WHERE STATUS=:status";
                    p = new OracleParameter[] { new OracleParameter("status", status) };
                }
                else p = new OracleParameter[0];
                sql += " ORDER BY INITIATION_DATE DESC";
                DataTable dt = DBHelper.ExecuteQuery(sql, p);
                gvList.DataSource = dt;
                gvList.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("List Category error: " + ex.Message);
                gvList.DataSource = new DataTable();
                gvList.DataBind();
            }
        }
    }
}
