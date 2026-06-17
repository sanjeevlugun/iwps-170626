using System;

namespace IWPS
{
    public partial class print_wo_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { if (!IsPostBack) LoadData("", ""); }

        void LoadData(string wono, string status)
        {
            try
            {
                string sql = "SELECT WO_NO,ENQ_NO,LOI_NO,CONTRACT_VALUE,STATUS,WO_DATE FROM MATPASS.TBL_IWPS_WO_HEADER WHERE 1=1";
                var prms = new System.Collections.Generic.List<Oracle.ManagedDataAccess.Client.OracleParameter>();
                if (!string.IsNullOrEmpty(wono)) { sql += " AND WO_NO LIKE :w"; prms.Add(DBHelper.P("w", "%" + wono + "%")); }
                if (!string.IsNullOrEmpty(status)) { sql += " AND STATUS=:s"; prms.Add(DBHelper.P("s", status)); }
                sql += " ORDER BY WO_DATE DESC";
                gvWO.DataSource = DBHelper.ExecuteQuery(sql, prms.ToArray());
                gvWO.DataBind();
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e) { LoadData(txtWONo.Text.Trim(), ddlStatus.SelectedValue); }
    }
}
