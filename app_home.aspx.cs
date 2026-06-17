using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace IWPS
{
    public partial class app_home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadDashboard();
        }

        private void LoadDashboard()
        {
            try
            {
                object cnt;
                cnt = DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MATPASS.TBL_IWPS_INDENT_HEADER WHERE STATUS='INITIATED'", new OracleParameter[0]);
                lblPendingIndent.Text = cnt != null ? cnt.ToString() : "0";

                cnt = DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MATPASS.TBL_IWPS_ENQUIRY_HEADER WHERE STATUS='INITIATED'", new OracleParameter[0]);
                lblPendingEnq.Text = cnt != null ? cnt.ToString() : "0";

                cnt = DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MATPASS.TBL_IWPS_LOI_HEADER WHERE STATUS='INITIATED'", new OracleParameter[0]);
                lblPendingLOI.Text = cnt != null ? cnt.ToString() : "0";

                cnt = DBHelper.ExecuteScalar("SELECT COUNT(*) FROM MATPASS.TBL_IWPS_WO_HEADER WHERE WO_STATUS='INITIATED'", new OracleParameter[0]);
                lblPendingWO.Text = cnt != null ? cnt.ToString() : "0";

                DataTable dtIndents = DBHelper.ExecuteQuery("SELECT * FROM (SELECT INDENT_NO, INDENT_DATE, TITLE, INDENT_DEPT, STATUS FROM MATPASS.TBL_IWPS_INDENT_HEADER ORDER BY INITIATION_DATE DESC) WHERE ROWNUM<=5", new OracleParameter[0]);
                gvRecentIndents.DataSource = dtIndents;
                gvRecentIndents.DataBind();

                DataTable dtWOs = DBHelper.ExecuteQuery("SELECT * FROM (SELECT WO_NO, WO_DATE, NATURE_OF_WORK, DEPT, WO_STATUS FROM MATPASS.TBL_IWPS_WO_HEADER ORDER BY WO_DATE DESC) WHERE ROWNUM<=5", new OracleParameter[0]);
                gvRecentWOs.DataSource = dtWOs;
                gvRecentWOs.DataBind();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Dashboard error: " + ex.Message);
                // Bind empty tables on error
                gvRecentIndents.DataSource = new DataTable();
                gvRecentIndents.DataBind();
                gvRecentWOs.DataSource = new DataTable();
                gvRecentWOs.DataBind();
            }
        }
    }
}
